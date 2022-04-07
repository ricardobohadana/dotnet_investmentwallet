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
    public class OperacaoRepository : IOperacaoRepository
    {
        private string _connectionString;

        public OperacaoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Alterar(Operacao entity)
        {
            string query = @"
            UPDATE OPERACAO
            SET
                IDCARTEIRA=@IdCarteira, IDTIPOOPERACAO=@IdTipoOperacao, IDTIPOATIVO=@IdTipoAtivo, NOMEATIVO=@NomeAtivo,
                SIGLAATIVO=@SiglaAtivo, DESCRICAOATIVO=@DescricaoAtivo, DATAOPERACAO=@DataOperacao, PRECOATIVO=@PrecoAtivo,
                QUANTIDADEATIVO=@QuantidadeAtivo, TOTAL=@Total
            WHERE
                IDOPERACAO=@IdOperacao              
            ";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Operacao> Consultar()
        {
            string query = @"SELECT * FROM OPERACAO ORDER BY DATAOPERACAO";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Operacao>(query).ToList();
            }
        }

        public void Excluir(Guid id)
        {
            string query = @"
                DELETE FROM OPERACAO WHERE IDOPERACAO=@id
            ";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new { id });
            }
        }

        public void Inserir(Operacao entity)
        {
            string query = @"
                INSERT INTO 
                    OPERACAO 
                    (IDOPERACAO, IDCARTEIRA, IDTIPOOPERACAO, IDTIPOATIVO, NOMEATIVO, SIGLAATIVO, DESCRICAOATIVO, DATAOPERACAO, PRECOATIVO, QUANTIDADEATIVO, TOTAL)
                    VALUES 
                    (@IdOperacao, @IdCarteira, @IdTipoOperacao, @IdTipoAtivo, @NomeAtivo, @SiglaAtivo, @DescricaoAtivo, @DataOperacao, @PrecoAtivo, @QuantidadeAtivo, @Total)
            ";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public Operacao ObterPorId(Guid id)
        {
            string query = @"SELECT * FROM OPERACAO WHERE IDOPERACAO=@id";

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Operacao>(query, new { id }).FirstOrDefault();
            }
        }

        public List<Operacao> ObterPorListaDeIdCarteiras(List<Guid> ids)
        {
            string query = @"
            SELECT 
	            OPERACAO.IDOPERACAO,
	            OPERACAO.DATAOPERACAO,
	            OPERACAO.NOMEATIVO,
	            OPERACAO.SIGLAATIVO,
	            OPERACAO.DESCRICAOATIVO,
	            OPERACAO.PRECOATIVO,
	            OPERACAO.QUANTIDADEATIVO,
	            OPERACAO.TOTAL,
	            TIPOOPERACAO.IDTIPOOPERACAO,
	            TIPOOPERACAO.NOME,
	            TIPOATIVO.IDTIPOATIVO,
	            TIPOATIVO.NOME,
	            CARTEIRA.IDCARTEIRA,
	            CARTEIRA.IDUSUARIO,
	            CARTEIRA.NOME,
	            CARTEIRA.DESCRICAO
            FROM OPERACAO
            INNER JOIN TIPOOPERACAO ON OPERACAO.IDTIPOOPERACAO=TIPOOPERACAO.IDTIPOOPERACAO
            INNER JOIN TIPOATIVO ON OPERACAO.IDTIPOATIVO=TIPOATIVO.IDTIPOATIVO
            INNER JOIN CARTEIRA ON OPERACAO.IDCARTEIRA=CARTEIRA.IDCARTEIRA";
            
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Operacao, TipoOperacao, TipoAtivo, Carteira, Operacao>(
                    query,
                    map: (op, tipoop, tipoat, cart) =>
                    {
                        op.TipoOperacao = tipoop;
                        op.TipoAtivo = tipoat;
                        op.Carteira = cart;

                        return op;
                    },
                    splitOn: "IDTIPOOPERACAO,IDTIPOATIVO,IDCARTEIRA",
                    param: new { ids }).ToList();
            }
        }

    }
}
