using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentWallet.Domain.Interfaces.Repositories
{
    public interface IBaseImutableRepository<TEntity>
        where TEntity : class
    {
        List<TEntity> Consultar();
        TEntity ObterPorId(Guid id);
    }
}
