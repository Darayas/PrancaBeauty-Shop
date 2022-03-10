using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ProdcutReviews
{

    public class OutGetReviewsForProductDetails
    {
        public string ProductTitle { get; set; }
        public List<OutGetReviewsForProductDetailsMedia> LstMedias { get; set; }

        public List<OutGetReviewsForProductDetailsItems> LstReviews { get; set; }
    }

    public class OutGetReviewsForProductDetailsItems
    {
        public string Id { get; set; }
        public string UserProfileImage { get; set; }
        public string UserFullName { get; set; }
        public DateTime Date { get; set; }
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
        public List<OutGetReviewsForProductDetailsSellers> LstSellers { get; set; }

        public List<OutGetReviewsForProductDetailsMedia> LstMedia { get; set; }
        public List<OutGetReviewsForProductDetailsAttributes> LstAttributes { get; set; }
    }

    public class OutGetReviewsForProductDetailsMedia
    {
        public string Id { get; set; }
        public string CommentId { get; set; }
        public string MimeType { get; set; }
        public string FileUrl { get; set; }
    }

    public class OutGetReviewsForProductDetailsAttributes
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
