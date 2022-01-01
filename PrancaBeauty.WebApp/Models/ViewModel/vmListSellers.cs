using System;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewModel
{
    public class vmListSellers
    {
        public string Id { get; set; }

        [Display(Name = "FullName")]
        public string FullName { get; set; }

        [Display(Name = "Date")]
        public string Date { get; set; }

        [Display(Name = "IsConfirm")]
        public bool IsConfirm { get; set; }
    }
}
