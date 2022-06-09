using Framework.Infrastructure;
using PrancaBeauty.Domin.PostalBarcodes.PostalBarcodeAgg.Contracts;
using PrancaBeauty.Domin.Users.AddressAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.PostalBarcode
{
    public class PostalBarcodeApplication: IPostalBarcodeApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IPostalBarcodeRepository _PostalBarcodeRepository;

        public PostalBarcodeApplication(ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider, IPostalBarcodeRepository postalBarcodeRepository)
        {
            _Logger=logger;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _PostalBarcodeRepository=postalBarcodeRepository;
        }
    }
}
