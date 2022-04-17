using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase
{

    public class InpSaveEditShowcase
    {
        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "UserId")]
        [RequiredString]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "CountryId")]
        [GUID]
        public string CountryId { get; set; }

        [Display(Name = "Name")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Name { get; set; }

        [Display(Name = "BackgroundColorCode")]
        [MaxLengthString(100)]
        public string BackgroundColorCode { get; set; }

        [Display(Name = "CssStyle")]
        [MaxLengthString(500)]
        public string CssStyle { get; set; }

        [Display(Name = "CssClass")]
        [MaxLengthString(100)]
        public string CssClass { get; set; }

        [Display(Name = "IsFullWidth")]
        public bool IsFullWidth { get; set; }

        [Display(Name = "IsEnable")]
        public bool IsEnable { get; set; }

        [Display(Name = "StartDate")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "EndDate")]
        public DateTime? EndDate { get; set; }

        public List<InpSaveEditShowcase_Translate> LstTranslate { get; set; }
    }

    public class InpSaveEditShowcase_Translate
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "Title")]
        [MaxLengthString(100)]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [MaxLengthString(100)]
        public string Description { get; set; }
    }
}
