using Dapper;
using InvestmentWallet.Domain.Entities;
using InvestmentWallet.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentWallet.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private string _connectionString;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Alterar(Usuario entity)
        {
            string query = @"UPDATE USUARIO SET
                NOME=@Nome, IDPERFILINVESTIDOR=@IdPerfilInvestidor
                WHERE IDUSUARIO=@IdUsuario";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Usuario> Consultar()
        {
            string query = @"SELECT * FROM USUARIO ORDER BY NOME";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Usuario>(query).ToList();
            }
        }

        public void Excluir(Guid id)
        {
            string query = @"DELETE FROM USUARIO WHERE IDUSUARIO=@id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new { id });
            }
        }

        public void Inserir(Usuario entity)
        {
            string query = @"INSERT INTO USUARIO (IDUSUARIO, IDPERFILINVESTIDOR, NOME, SENHA, EMAIL) VALUES (@IdUsuario, @IdPerfilInvestidor, @Nome, @Senha, @Email)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public Usuario Obter(string email)
        {
            string query = @"SELECT * FROM USUARIO WHERE EMAIL=@email";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Usuario>(query, new { email }).FirstOrDefault();
            }
        }

        public Usuario Obter(string email, string senha)
        {
            string query = @"SELECT * FROM USUARIO WHERE EMAIL=@email AND SENHA=@senha";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Usuario>(query, new { email, senha}).FirstOrDefault();
            }
        }

        public Usuario ObterPorId(Guid id)
        {
            string query = @"SELECT * FROM USUARIO WHERE IDUSUARIO=@id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Usuario>(query, new {id}).FirstOrDefault();
            }
        }
    }
}
