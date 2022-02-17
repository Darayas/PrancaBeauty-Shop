using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ProdcutReviews
{
    public class OutGetReviewsForProductDetails
    {
        public string Id { get; set; }
        public string UserFullName { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string Advantages { get; set; } // نقاط قوت
        public string DisAdvantages { get; set; } // نقاط ضعف
        public bool IsRead { get; set; }
        public bool IsConfirm { get; set; }

        public string SellerId { get; set; }
        public string SellerFullName { get; set; }
        public string SellerImgUrl { get; set; }

        public int CountLikes { get; set; }
        public int CountDislike { get; set; }

        public List<OutGetReviewsForProductDetailsMedia> LstMedia { get; set; }
        public List<OutGetReviewsForProductDetailsAttributes> LstAttributes { get; set; }
    }

    public class OutGetReviewsForProductDetailsMedia
    {
        public string Id { get; set; }
        public string FileUrl { get; set; }
    }

    public class OutGetReviewsForProductDetailsAttributes
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }
}
