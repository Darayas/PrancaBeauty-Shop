using Framework.Infrastructure;
using PrancaBeauty.Domin.PostalBarcodes.PostalBarcodeAgg.Contracts;
using PrancaBeauty.Domin.PostalBarcodes.PostalBarcodeAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.PostalBarcode
{
    public class PostalBarcodeRepository : BaseRepository<tblPostalBarcodes>, IPostalBarcodeRepository
    {
        public PostalBarcodeRepository(MainContext Context) : base(Context)
        {

        }
    }
}
