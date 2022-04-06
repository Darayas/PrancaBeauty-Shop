using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmProductList
    {
        public string Id { get; set; }
        public string AuthorUserId { get; set; }
        public string AuthorUserName { get; set; }

        [Display(Name= "AuthorName")]
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }
        public string UniqueNumber { get; set; }
        public string Name { get; set; }

        [Display(Name= "Title")]
        public string Title { get; set; }

        [Display(Name= "ImgUrl")]
        public string ImgUrl { get; set; }
        public string CategoryName { get; set; }
        public string CategoryTitle { get; set; }
        public string CategoryMap { get; set; }
        public string CategoryId { get; set; }

        [Display(Name= "Status")]
        public int Status { get; set; }
        public bool IsDelete { get; set; }
        public bool IsConfirm { get; set; }
        public bool IsDraft { get; set; }
        public bool ItsForConfirm { get; set; }
        public string Date { get; set; }
        public bool HasUnConfirmedSellerRequest { get; set; }
        public int CountSellers { get; set; }
        public bool HasUnConfirmedReviews { get; set; }
        public int CountReviews { get; set; }
        public bool HasUnConfirmedAsk { get; set; }
        public int CountAsks { get; set; }
        public int CountVisit { get; set; }
    }

    public enum vmProductList_Status
    {
        IsDelete = 0,
        IsDraft = 1,
        IsConfirm = 2,
        IsSchedule = 3,
        UnKnown = 4
    }
}
