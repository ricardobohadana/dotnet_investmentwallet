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
    public class TipoAtivoRepository : ITipoAtivoRepository
    {
        private string _connectionString;

        public TipoAtivoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<TipoAtivo> Consultar()
        {
            string query = @"SELECT * FROM TIPOATIVO";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<TipoAtivo>(query).ToList();
            }
        }

        public TipoAtivo ObterPorId(Guid id)
        {
            string query = @"SELECT * FROM TIPOATIVO WHERE IDTIPOATIVO=@id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<TipoAtivo>(query, new { id }).FirstOrDefault();
            }
        }

        public TipoAtivo ObterPorNome(string nome)
        {
            string query = @"SELECT * FROM TIPOATIVO WHERE NOME=@nome";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<TipoAtivo>(query, new { nome }).FirstOrDefault();
            }
        }
    }
}
