using Framework.Infrastructure;
using PrancaBeauty.Domin.Region.CityAgg.Contracts;
using PrancaBeauty.Domin.Region.CityAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Cities
{
    public class CityRepository : BaseRepository<tblCities>, ICityRepository
    {
        public CityRepository(MainContext context) : base(context)
        {

        }
    }
}
