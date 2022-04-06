using System.Collections.Generic;
using System;
using PrancaBeauty.Application.Contracts.ApplicationDTO.ProdcutReviews;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmCompo_ListProductReviews
    {
        public string ProductTitle { get; set; }

        public List<vmCompo_ListProductReviewsAttributes> LstAttributes { get; set; }
        public List<vmCompo_ListProductReviewsMedia> LstMedias { get; set; } = new List<vmCompo_ListProductReviewsMedia>();
        public List<vmCompo_ListProductReviewsItems> LstReviews { get; set; } = new List<vmCompo_ListProductReviewsItems>();
    }

    public class vmCompo_ListProductReviewsItems
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

        public bool IsLike { get; set; }
        public int CountLikes { get; set; }

        public bool IsDisLike { get; set; }
        public int CountDislike { get; set; }

        public bool IsBuyer { get; set; }
        public List<vmGetReviewsForProductDetailsSellers> LstSellers { get; set; }

        public List<vmCompo_ListProductReviewsMedia> LstMedia { get; set; }

    }

    public class vmCompo_ListProductReviewsMedia
    {
        public string Id { get; set; }
        public string CommentId { get; set; }
        public string MimeType { get; set; }
        public string FileUrl { get; set; }
    }

    public class vmCompo_ListProductReviewsAttributes
    {
        public string Title { get; set; }
        public double Avg { get; set; }
    }

    public class vmGetReviewsForProductDetailsSellers
    {
        public string SellerId { get; set; }
        public string SellerFullName { get; set; }
        public string SellerImgUrl { get; set; }
        public string VariantType { get; set; }
    }
}
