using InvestmentWallet.Domain.Entities;
using InvestmentWallet.Domain.Interfaces.Repositories;
using InvestmentWallet.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentWallet.Domain.Services
{
    public class UsuarioDomainService : IUsuarioDomainService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPerfilInvestidorRepository _perfilInvestidorRepository;
        private readonly ICarteiraRepository _carteiraRepository;

        public UsuarioDomainService(IUsuarioRepository usuarioRepository, IPerfilInvestidorRepository perfilInvestidorRepository)
        {
            _usuarioRepository = usuarioRepository;
            _perfilInvestidorRepository = perfilInvestidorRepository;
        }


        private string CriptografarSenha(string senha)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: senha,
                salt: new byte[128 / 8],
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10,
                numBytesRequested: 256 / 8
                )
            );
        }

        private bool VerificarSenha(string senhaNova, string senhaCriptografada)
        {
            string senhaNovaCriptografada = CriptografarSenha(senhaNova);
            return senhaNovaCriptografada == senhaCriptografada;
        }


        public Usuario AutenticarUsuario(string email, string senha)
        {
            Usuario usuario = _usuarioRepository.Obter(email);
            if (usuario == null)
                throw new Exception($"O email {email} não está cadastrado.");

            bool acertouSenha = VerificarSenha(senha, usuario.Senha);

            if (!acertouSenha)
                throw new Exception($"A combinação de email e senha informados está incorreta.");

            return usuario;
            
        }

        public void CadastrarUsuario(Usuario usuario)
        {
            // REGRA DE PROJETO 1: NÃO CADASTRAR DOIS USUÁRIOS COM O MESMO EMAIL
            if (_usuarioRepository.Obter(usuario.Email) != null)
            {
                throw new Exception($"O email '{usuario.Email}' já está cadastrado na aplicação.");
            }

            // REGRA DE PROJETO 2: CADASTRAR USUÁRIO COM PERFIL INICIAL CONSERVADOR
            PerfilInvestidor perfilInvestidor = _perfilInvestidorRepository.ObterPorTipo("Conservador");
            usuario.PerfilInvestidor = perfilInvestidor;
            usuario.IdPerfilInvestidor = perfilInvestidor.IdPerfilInvestidor;

            // REGRA DE PROJETO 3: CRIPTOGRAFAR A SENHA DO USUARIO
            usuario.Senha = CriptografarSenha(usuario.Senha);

            // REGRA DE PROJETO 4: INSERIR USUÁRIO COM LISTA DE CARTEIRA VAZIA
            usuario.Carteiras = new List<Carteira>();

            // Inserir usuario no banco de dados
            _usuarioRepository.Inserir(usuario);




        }

        public Usuario ObterUsuarioPorId(Guid id)
        {
            // Perguntar se essa é a melhor prática para conseguir os dados do usuário, uma vez que acessar o banco de dados 
            // é uma tarefa custosa e, quanto maior a quantidade de relacionamentos, maior a quantidade de acessos (nessa arquitetura)
            // e maior a demora.

            Usuario usuario = _usuarioRepository.ObterPorId(id);
            usuario.Carteiras = _carteiraRepository.ObterPorIdUsuario(usuario.IdUsuario);
            usuario.PerfilInvestidor = _perfilInvestidorRepository.ObterPorId(usuario.IdPerfilInvestidor);

            return usuario;
        }
    }
}
