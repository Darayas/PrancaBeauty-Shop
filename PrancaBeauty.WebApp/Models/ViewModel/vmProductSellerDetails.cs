using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewModel
{
    public class vmProductSellerDetails
    {
        public string Id { get; set; }

        [Display(Name = "FulUserName")]
        public string FulUserName { get; set; }

        [Display(Name = "SellerTitle")]
        public string SellerTitle { get; set; }

        [Display(Name = "RegDateTime")]
        public string DateTime { get; set; }

        [Display(Name = "SellerLogoUrl")]
        public string SellerLogoUrl { get; set; }

        [Display(Name = "IsSellerConfimed")]
        public bool IsSellerConfimed { get; set; }

        public string ProductTitle { get; set; }
        public string ProductName { get; set; }
    }
}
