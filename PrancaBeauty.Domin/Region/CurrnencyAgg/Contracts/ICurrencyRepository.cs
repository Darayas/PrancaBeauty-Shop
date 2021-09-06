using Framework.Domain;
using PrancaBeauty.Domin.Region.CurrnencyAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Region.CurrnencyAgg.Contracts
{
    public interface ICurrencyRepository : IRepository<tblCurrencies>
    {
    }
}
