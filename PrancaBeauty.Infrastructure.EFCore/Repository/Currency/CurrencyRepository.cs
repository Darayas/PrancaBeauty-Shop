using Framework.Infrastructure;
using PrancaBeauty.Domin.Region.CurrnencyAgg.Contracts;
using PrancaBeauty.Domin.Region.CurrnencyAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Currency
{
    public class CurrencyRepository : BaseRepository<tblCurrencies>, ICurrencyRepository
    {
        public CurrencyRepository(MainContext Context) : base(Context)
        {

        }
    }
}
