using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentWallet.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> : IBaseImutableRepository<TEntity>
        where TEntity : class
    {
        void Alterar(TEntity entity);
        void Inserir(TEntity entity);
        void Excluir(Guid id);
    }
}
