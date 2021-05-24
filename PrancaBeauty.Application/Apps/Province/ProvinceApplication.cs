using PrancaBeauty.Domin.Region.ProvinceAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Province
{
    public class ProvinceApplication : IProvinceApplication
    {
        private readonly IProvinceRepository _ProvinceRepository;
        public ProvinceApplication(IProvinceRepository provinceRepository)
        {
            _ProvinceRepository = provinceRepository;
        }
    }
}
