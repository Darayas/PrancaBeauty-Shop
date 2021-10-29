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
    public class GuaranteeRepository : BaseRepository<tblGuarantee>, IGuaranteeRepository
    {
        public GuaranteeRepository(MainContext Context) : base(Context)
        {

        }
    }
}
