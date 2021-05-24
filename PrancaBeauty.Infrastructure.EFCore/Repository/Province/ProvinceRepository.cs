using Framework.Infrastructure;
using PrancaBeauty.Domin.Region.ProvinceAgg.Contracts;
using PrancaBeauty.Domin.Region.ProvinceAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Province
{
    public class ProvinceRepository : BaseRepository<tblProvinces>, IProvinceRepository
    {
        public ProvinceRepository(MainContext context) : base(context)
        {

        }
    }
}
