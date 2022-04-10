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
    public class TipoOperacaoRepository: ITipoOperacaoRepository
    {
        private string _connectionString;
        private bool isDev;

        public TipoOperacaoRepository(string connectionString)
        {
            _connectionString = connectionString;
            isDev = false;
        }

        public List<TipoOperacao> Consultar()
        {
            string query = @"SELECT * FROM TIPOOPERACAO";

            using (var connection = DatabaseSqlConnection.GetConnection(_connectionString, isDev))
            {
                return connection.Query<TipoOperacao>(query).ToList();
            }
        }

        public TipoOperacao ObterPorId(Guid id)
        {
            string query = @"SELECT * FROM TIPOOPERACAO WHERE IDTIPOOPERACAO=@id";

            using (var connection = DatabaseSqlConnection.GetConnection(_connectionString, isDev))
            {
                return connection.Query<TipoOperacao>(query, new { id }).FirstOrDefault();
            }
        }

        public TipoOperacao ObterPorNome(string nome)
        {
            string query = @"SELECT * FROM TIPOOPERACAO WHERE NOME=@nome";

            using (var connection = DatabaseSqlConnection.GetConnection(_connectionString, isDev))
            {
                return connection.Query<TipoOperacao>(query, new { nome }).FirstOrDefault();
            }
        }
    }
}
