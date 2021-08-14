using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.ProductTopicAgg.Contracts;
using PrancaBeauty.Domin.Product.ProductTopicAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.ProductTopic
{
    public class ProductTopic_TranslatesRepository : BaseRepository<tblProductTopic_Translates>, IProductTopic_TranslatesRepository
    {
        public ProductTopic_TranslatesRepository(MainContext Context) : base(Context)
        {

        }
    }
}
