using Framework.Infrastructure;
using PrancaBeauty.Domin.Product.GuaranteeAgg.Contracts;
using PrancaBeauty.Domin.Product.GuaranteeAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Guarantee
{
    public class Guarantee_TranslatesRepository : BaseRepository<tblGuarantee_Translates>, IGuarantee_TranslatesRepository
    {
        public Guarantee_TranslatesRepository(MainContext Context) : base(Context)
        {

        }
    }
}
