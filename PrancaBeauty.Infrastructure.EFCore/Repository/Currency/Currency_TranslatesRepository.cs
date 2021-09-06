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
    public class Currency_TranslatesRepository : BaseRepository<tblCurrency_Translates>, ICurrency_TranslatesRepository
    {
        public Currency_TranslatesRepository(MainContext Context) : base(Context)
        {

        }
    }
}
