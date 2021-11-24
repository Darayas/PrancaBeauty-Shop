using Framework.Domain;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Region.CountryAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.PostingRestrictionsAgg.Entites
{
    public class tblPostingRestrictions : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid CountryId { get; set; }
        public bool Posting { get; set; } // ارسال میشود؟ ,ارسال نمیشود؟

        public virtual tblProducts tblProducts { get; set; }
        public virtual tblCountries tblCountries { get; set; }
    }
}
