using Framework.Domain;
using PrancaBeauty.Domin.Region.ProvinceAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Region.CityAgg.Entities
{
    public class tblCities : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProvinceId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual tblProvinces tblProvince { get; set; }
        public virtual ICollection<tblCities_Translates> tblCities_Translates { get; set; }
    }
}
