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
    public class PerfilInvestidorRepository : IPerfilInvestidorRepository
    {
        private string _connectionString;

        public PerfilInvestidorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<PerfilInvestidor> Consultar()
        {
            string query = @"SELECT * FROM PERFILINVESTIDOR";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<PerfilInvestidor>(query).ToList();
            }
        }


        public PerfilInvestidor ObterPorId(Guid id)
        {
            string query = @"SELECT * FROM PERFILINVESTIDOR WHERE ID=@id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<PerfilInvestidor>(query, new { id }).FirstOrDefault();
            }
        }

        public PerfilInvestidor ObterPorTipo(string tipo)
        {
            string query = @"SELECT * FROM PERFILINVESTIDOR WHERE TIPO=@Tipo";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<PerfilInvestidor>(query, new {tipo}).FirstOrDefault();
            }
        }
    }
}
