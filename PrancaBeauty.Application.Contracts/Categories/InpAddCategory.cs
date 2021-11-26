using Framework.Common.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Categories
{
    public class InpAddCategory
    {
        [Display(Name = "UserId")]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "ParentId")]
        [GUID]
        public string ParentId { get; set; }

        [Display(Name = "CategoryImage")]
        [FileSize(102400, ErrorMessage = "FileSizeMsg")]
        [AllowExtentions("image/jpg,image/jpeg", ErrorMessage = "AllowExtentionsMsg")]
        public IFormFile Image { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(100, ErrorMessage = "MaxLengthMsg")]
        public string Name { get; set; }

        [Display(Name = "Sort")]
        [Range(1, int.MaxValue, ErrorMessage = "RangeMsg")]
        public int Sort { get; set; }

        public List<InpAddCategory_Translate> LstTranslate { get; set; }
    }

    public class InpAddCategory_Translate
    {
        [Display(Name = "LangId")]
        [Required(ErrorMessage = "RequiredMsg")]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "RequiredMsg")]
        [MaxLength(200, ErrorMessage = "MaxLengthMsg")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}

