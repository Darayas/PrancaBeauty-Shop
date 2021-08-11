using Framework.Domain;
using Microsoft.AspNetCore.Identity;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Product.ProductAskAgg.Entities;
using PrancaBeauty.Domin.Product.ProductAskLikesAgg.Entities;
using PrancaBeauty.Domin.Product.ProductPricesAgg.Entities;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Entities;
using PrancaBeauty.Domin.Product.ProductReviewsLikesAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Entities;
using PrancaBeauty.Domin.Users.AddressAgg.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrancaBeauty.Domin.Users.UserAgg.Entities
{
    public class tblUsers : IdentityUser<Guid>, IEntity
    {
        public Guid? LangId { get; set; }
        public Guid AccessLevelId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime Date { get; set; }
        public string PasswordPhoneNumber { get; set; }
        public DateTime? LastTrySentSms { get; set; }
        public bool IsActive { get; set; }

        public virtual tblAccessLevels tblAccessLevels { get; set; }
        public virtual tblLanguages tblLanguages { get; set; }
        public virtual ICollection<tblFiles> tblFiles { get; set; }
        public virtual ICollection<tblAddress> tblAddress { get; set; }
        public virtual ICollection<tblProducts> tblProducts { get; set; }
        public virtual ICollection<tblProductPrices> tblProductPrices { get; set; }
        public virtual ICollection<tblProductReviews> tblProductReviews { get; set; }
        public virtual ICollection<tblProductReviewsLikes> tblProductReviewsLikes { get; set; }
        public virtual ICollection<tblProductAsk> tblProductAsk { get; set; }
        public virtual ICollection<tblProductAskLikes> tblProductAskLikes { get; set; }
    }
}
