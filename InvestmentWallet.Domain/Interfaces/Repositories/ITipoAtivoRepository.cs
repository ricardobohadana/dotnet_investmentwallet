﻿using InvestmentWallet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentWallet.Domain.Interfaces.Repositories
{
    public interface ITipoAtivoRepository: IBaseImutableRepository<TipoAtivo>
    {
        TipoAtivo ObterPorNome(string nome);
    }
}
