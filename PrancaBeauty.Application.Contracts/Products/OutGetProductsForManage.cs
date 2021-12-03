using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Products
{
    public class OutGetProductsForManage
    {
        public string Id { get; set; }
        public string AuthorUserId { get; set; }
        public string AuthorUserName { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }
        public string UniqueNumber { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public string CategoryName { get; set; }
        public string CategoryTitle { get; set; }
        public string CategoryId { get; set; }
        public OutGetProductsForManage_Status Status { get; set; }
        public bool IsDelete { get; set; }
        public bool IsConfirm { get; set; }
        public bool IsDraft { get; set; }
        public bool ItsForConfirm { get; set; }
        public DateTime Date { get; set; }
        public bool HasUnConfirmedSellerRequest { get; set; }
        public int CountSellers { get; set; }
        public bool HasUnConfirmedReviews { get; set; }
        public int CountReviews { get; set; }
        public bool HasUnConfirmedAsk { get; set; }
        public int CountVisit { get; set; }
        public int CountAsks { get; set; }
    }

    public enum OutGetProductsForManage_Status
    {
        IsDelete = 0,
        IsDraft = 1,
        IsConfirm = 2,
        ItsForConfirm = 3,
        IsSchedule = 4,
        UnKnown = 5
    }
}
