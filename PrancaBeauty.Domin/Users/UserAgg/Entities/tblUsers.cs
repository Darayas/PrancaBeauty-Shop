using Framework.Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using PrancaBeauty.Domin.BankAccount.BankAccountAgg.Entities;
using PrancaBeauty.Domin.Bills.BillAgg.Entities;
using PrancaBeauty.Domin.Cart.CartAgg.Entities;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.Product.ProductAgg.Entities;
using PrancaBeauty.Domin.Product.ProductAskAgg.Entities;
using PrancaBeauty.Domin.Product.ProductAskLikesAgg.Entities;
using PrancaBeauty.Domin.Product.ProductPricesAgg.Entities;
using PrancaBeauty.Domin.Product.ProductReviewsAgg.Entities;
using PrancaBeauty.Domin.Product.ProductReviewsLikesAgg.Entities;
using PrancaBeauty.Domin.Product.ProductSellerAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using PrancaBeauty.Domin.Showcases.ShowcaseAgg.Entities;
using PrancaBeauty.Domin.Sliders.SliderAgg.Entities;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Entities;
using PrancaBeauty.Domin.Users.AddressAgg.Entities;
using PrancaBeauty.Domin.Users.SellerAgg.Entities;
using PrancaBeauty.Domin.Wallet.WalletAgg.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrancaBeauty.Domin.Users.UserAgg.Entities
{
    public class tblUsers : IdentityUser<Guid>, IEntity
    {
        public Guid? LangId { get; set; }
        public Guid? ProfileImgId { get; set; }
        public Guid AccessLevelId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime Date { get; set; }
        public string PasswordPhoneNumber { get; set; }
        public DateTime? LastTrySentSms { get; set; }
        public bool IsActive { get; set; }
        public bool IsSeller { get; set; }

        public virtual tblAccessLevels tblAccessLevels { get; set; }
        public virtual tblFiles tblProfileImage { get; set; }
        public virtual tblLanguages tblLanguages { get; set; }
        public virtual tblSellers tblSellers { get; set; }
        public virtual ICollection<tblFiles> tblFiles { get; set; }
        public virtual ICollection<tblAddress> tblAddress { get; set; }
        public virtual ICollection<tblProducts> tblProducts { get; set; }
        public virtual ICollection<tblProductPrices> tblProductPrices { get; set; }
        public virtual ICollection<tblProductReviews> tblProductReviews { get; set; }
        public virtual ICollection<tblProductReviewsLikes> tblProductReviewsLikes { get; set; }
        public virtual ICollection<tblProductAsk> tblProductAsk { get; set; }
        public virtual ICollection<tblProductAskLikes> tblProductAskLikes { get; set; }
        public virtual ICollection<tblSlider> tblSlider { get; set; }
        public virtual ICollection<tblShowcases> tblShowCases { get; set; }
        public virtual ICollection<tblCarts> tblCarts { get; set; }
        public virtual ICollection<tblBills> tblBills { get; set; }
        public virtual ICollection<tblWallets> tblWallets { get; set; }
        public virtual ICollection<tblBankAccounts> tblBankAccounts { get; set; }

    }
}
