using System;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmSliderList
    {
        [Display(Name = "Id")]
        public string Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "ImgUrl")]
        public string ImgUrl { get; set; }

        [Display(Name = "Url")]
        public string Url { get; set; }

        [Display(Name = "Sort")]
        public int Sort { get; set; }

        [Display(Name = "IsEnable")]
        public bool IsEnable { get; set; }

        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(Name = "StartDate")]
        public string StartDate { get; set; }

        [Display(Name = "EndDate")]
        public string EndDate { get; set; }
    }
}
