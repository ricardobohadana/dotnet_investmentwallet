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
    public class TipoOperacaoRepository: ITipoOperacaoRepository
    {
        private string _connectionString;

        public TipoOperacaoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<TipoOperacao> Consultar()
        {
            string query = @"SELECT * FROM TIPOOPERACAO";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<TipoOperacao>(query).ToList();
            }
        }

        public TipoOperacao ObterPorId(Guid id)
        {
            string query = @"SELECT * FROM TIPOOPERACAO WHERE IDTIPOOPERACAO=@id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<TipoOperacao>(query, new { id }).FirstOrDefault();
            }
        }

        public TipoOperacao ObterPorNome(string nome)
        {
            string query = @"SELECT * FROM TIPOOPERACAO WHERE NOME=@nome";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<TipoOperacao>(query, new { nome }).FirstOrDefault();
            }
        }
    }
}
