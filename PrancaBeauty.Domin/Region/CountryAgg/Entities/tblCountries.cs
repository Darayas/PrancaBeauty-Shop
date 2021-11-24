using Framework.Domain;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.Product.PostingRestrictionsAgg.Entites;
using PrancaBeauty.Domin.Product.ProductPricesAgg.Entities;
using PrancaBeauty.Domin.Region.CurrnencyAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Domin.Region.ProvinceAgg.Entities;
using PrancaBeauty.Domin.Users.AddressAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Region.CountryAgg.Entities
{
    public class tblCountries : IEntity
    {
        public Guid Id { get; set; }
        public Guid FlagImgId { get; set; }
        public string Name { get; set; }
        public string PhoneCode { get; set; }
        public bool IsActive { get; set; }

        public virtual tblFiles tblFiles { get; set; }
        public virtual ICollection<tblCountries_Translates> tblCountries_Translates { get; set; }
        public virtual ICollection<tblAddress> tblAddress { get; set; }
        public virtual ICollection<tblProvinces> tblProvinces { get; set; }
        public virtual ICollection<tblLanguages> tblLanguages { get; set; }
        public virtual ICollection<tblCurrencies> tblCurrencies { get; set; }
        public virtual ICollection<tblPostingRestrictions> tblPostingRestrictions { get; set; }
    }
}
