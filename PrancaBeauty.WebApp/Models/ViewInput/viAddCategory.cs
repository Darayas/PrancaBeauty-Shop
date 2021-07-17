using Microsoft.AspNetCore.Http;
using PrancaBeauty.WebApp.Common.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viAddCategory
    {
        [Display(Name = "ParentId")]
        public string ParentId { get; set; }

        [Display(Name = "CategoryImage")]
        [RequiredFile(ErrorMessage = "RequiredFileMsg")]
        [FileSize(102400, ErrorMessage = "FileSizeMsg")]
        public IFormFile Image { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "StringLengthMsg")]
        public string Name { get; set; }

        [Display(Name = "Sort")]
        public int Sort { get; set; }

        [Required(ErrorMessage = "RequiredMsg")]
        public List<viAddCategory_Translate> LstTranslate { get; set; }
    }

    public class viAddCategory_Translate
    {
        [Display(Name = "LangId")]
        [Required(ErrorMessage = "RequiredMsg")]
        public string LangId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "RequiredMsg")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "StringLengthMsg")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
