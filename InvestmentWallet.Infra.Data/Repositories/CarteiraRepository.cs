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
    public class CarteiraRepository : ICarteiraRepository
    {
        string _connectionString;

        public CarteiraRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Alterar(Carteira entity)
        {
            string query = @"
                UPDATE CARTEIRA
                    SET NOME=@Nome, DESCRICAO=@Descricao
                    WHERE IDCARTEIRA=@IdCarteira
            ";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Carteira> Consultar()
        {
            throw new NotImplementedException();
        }

        public void Excluir(Guid id)
        {
            string query = @"
                DELETE FROM CARTEIRA WHERE IDCARTEIRA=@id
            ";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new { id });
            }
        }

        public void Inserir(Carteira entity)
        {
            string query = @"
                INSERT INTO CARTEIRA (IDCARTEIRA, IDUSUARIO, NOME, DESCRICAO)
                    VALUES (@IdCarteira, @IdUsuario, @Nome, @Descricao)
            ";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public Carteira ObterPorId(Guid id)
        {
            string query = @"
                SELECT * FROM CARTEIRA WHERE IDCARTEIRA=@id
            ";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Carteira>(query, new { id }).FirstOrDefault();
            }
        }

        public List<Carteira> ObterPorIdUsuario(Guid id)
        {
            string query = @"
                SELECT * FROM CARTEIRA WHERE IDUSUARIO=@id 
            ";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Carteira>(query, new { id }).ToList();
            }
        }
    }
}

// Perguntas

// Único Usuario -> Muitas Carteiras


// Devo consultar diretamente as carteiras de acordo com o idusuario, ou posso retornar do banco de dados todas as carteiras e filtrá-las em código C#?
// Qual é a melhor prática?
// Caso o primeiro caso, então não faria muito sentido manter o método Consultar herdado de IBaseRepository... Como funcionaria então?

// Há TABELAS, em meu banco de dados, que usam o campo IDCARTEIRA como FOREIGNKEY.
// Como trabalhar com exclusão de uma carteira se propagando para exclusão de todos os dados que tenham aquele IDCARTEIRA?