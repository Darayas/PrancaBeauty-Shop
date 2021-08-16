using Framework.Domain;
using PrancaBeauty.Domin.Categories.CategoriesAgg.Entities;
using PrancaBeauty.Domin.Keywords.Keywords_Products.Entities;
using PrancaBeauty.Domin.Product.ProductAskAgg.Entities;
using PrancaBeauty.Domin.Product.ProductMediaAgg.Entities;
using PrancaBeauty.Domin.Product.ProductPricesAgg.Entities;
using PrancaBeauty.Domin.Product.ProductPropertiesValuesAgg.Entities;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Entities;
using PrancaBeauty.Domin.Product.ProductSellerAgg.Entities;
using PrancaBeauty.Domin.Product.ProductVariantsItemsAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Product.ProductAgg.Entities
{
    public class tblProducts : IEntity
    {
        public Guid Id { get; set; }
        public Guid AuthorUserId { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; } // Uniqe Name
        public string Title { get; set; }
        public string Descreption { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsDraft { get; set; }
        public bool IsDelete { get; set; }

        public virtual tblUsers tblUsers { get; set; }
        public virtual tblCategoris tblCategoris { get; set; }
        public virtual ICollection<tblKeywords_Products> tblKeywords_Products { get; set; }
        public virtual ICollection<tblProductPrices> tblProductPrices { get; set; }
        public virtual ICollection<tblProductMedia> tblProductMedia { get; set; }
        public virtual ICollection<tblProductReviews> tblProductReviews { get; set; }
        public virtual ICollection<tblProductAsk> tblProductAsk { get; set; }
        public virtual ICollection<tblProductPropertiesValues> tblProductPropertiesValues { get; set; }
        public virtual ICollection<tblProductVariantItems> tblProductVariantItems { get; set; }
        public virtual ICollection<tblProductSellers> tblProductSellers { get; set; }


    }
}
