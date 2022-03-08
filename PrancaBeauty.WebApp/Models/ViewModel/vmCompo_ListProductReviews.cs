using System.Collections.Generic;
using System;
using PrancaBeauty.Application.Contracts.ProdcutReviews;

namespace PrancaBeauty.WebApp.Models.ViewModel
{
    public class vmCompo_ListProductReviews
    {
        public string Id { get; set; }
        public string UserProfileImage { get; set; }
        public string UserFullName { get; set; }
        public string Date { get; set; }
        public string Text { get; set; }
        public string Advantages { get; set; } // نقاط قوت
        public string DisAdvantages { get; set; } // نقاط ضعف
        public bool IsRead { get; set; }
        public bool IsConfirm { get; set; }

        public int CountStar { get; set; }

        public int CountLikes { get; set; }
        public int CountDislike { get; set; }

        public bool IsBuyer { get; set; }
        public List<OutGetReviewsForProductDetailsSellers> LstSellers { get; set; }

        public List<vmCompo_ListProductReviewsMedia> LstMedia { get; set; }
        public List<vmCompo_ListProductReviewsAttributes> LstAttributes { get; set; }
    }

    public class vmCompo_ListProductReviewsMedia
    {
        public string Id { get; set; }
        public string FileUrl { get; set; }
    }

    public class vmCompo_ListProductReviewsAttributes
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }

    public class OutGetReviewsForProductDetailsSellers
    {
        public string SellerId { get; set; }
        public string SellerFullName { get; set; }
        public string SellerImgUrl { get; set; }
        public string VariantType { get; set; }
    }
}
