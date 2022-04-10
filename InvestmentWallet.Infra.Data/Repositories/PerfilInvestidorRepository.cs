using Dapper;
using InvestmentWallet.Domain.Entities;
using InvestmentWallet.Domain.Interfaces.Repositories;
using InvestmentWallet.Infra.Data.Database;
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
        private bool isDev;

        public PerfilInvestidorRepository(string connectionString)
        {
            _connectionString = connectionString;
            isDev = false;
        }

        public List<PerfilInvestidor> Consultar()
        {
            string query = @"SELECT * FROM PERFILINVESTIDOR";

            using (var connection = DatabaseSqlConnection.GetConnection(_connectionString, isDev))
            {
                return connection.Query<PerfilInvestidor>(query).ToList();
            }
        }


        public PerfilInvestidor ObterPorId(Guid id)
        {
            string query = @"SELECT * FROM PERFILINVESTIDOR WHERE ID=@id";

            using (var connection = DatabaseSqlConnection.GetConnection(_connectionString, isDev))
            {
                return connection.Query<PerfilInvestidor>(query, new { id }).FirstOrDefault();
            }
        }

        public PerfilInvestidor ObterPorTipo(string tipo)
        {
            string query = @"SELECT * FROM PERFILINVESTIDOR WHERE TIPO=@Tipo";

            using (var connection = DatabaseSqlConnection.GetConnection(_connectionString, isDev))
            {
                return connection.Query<PerfilInvestidor>(query, new {tipo}).FirstOrDefault();
            }
        }
    }
}
