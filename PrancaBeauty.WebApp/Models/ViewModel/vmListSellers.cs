using System;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewModel
{
    public class vmListSellers
    {
        public string Id { get; set; }

        [Display(Name = "SellerFullName")]
        public string FullName { get; set; }

        [Display(Name = "Date")]
        public string Date { get; set; }

        [Display(Name = "IsConfirmed")]
        public bool IsConfirm { get; set; }

        [Display(Name = "VariantId")]
        public string VariantId { get; set; }
    }
}
