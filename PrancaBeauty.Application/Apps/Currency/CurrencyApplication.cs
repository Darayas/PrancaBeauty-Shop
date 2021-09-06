using PrancaBeauty.Domin.Region.CurrnencyAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Currency
{
    public class CurrencyApplication : ICurrencyApplication
    {
        private readonly ICurrencyRepository _CurrencyRepository;

        public CurrencyApplication(ICurrencyRepository currencyRepository)
        {
            _CurrencyRepository = currencyRepository;
        }
    }
}
