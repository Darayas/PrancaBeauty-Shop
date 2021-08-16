using Framework.Infrastructure;
using PrancaBeauty.Domin.Users.SellerAgg.Contracts;
using PrancaBeauty.Domin.Users.SellerAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Sellers
{
    public class Seller_TranslatesRepository : BaseRepository<tblSeller_Translates>, ISeller_TranslatesRepository
    {
        public Seller_TranslatesRepository(MainContext Context) : base(Context)
        {

        }
    }
}
