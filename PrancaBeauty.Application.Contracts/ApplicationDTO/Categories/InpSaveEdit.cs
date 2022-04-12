﻿using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Categories
{
    public class InpSaveEdit
    {
        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "UserId")]
        [GUID]
        public string UserId { get; set; }

        [Display(Name = "ParentId")]
        [GUID]
        public string ParentId { get; set; }

        [Display(Name = "CategoryImage")]
        [FileSize(102400)]
        [AllowExtentions("image/jpg,image/jpeg")]
        public IFormFile Image { get; set; }

        [Display(Name = "ImgCategoryUrl")]
        public string ImgCategoryUrl { get; set; }

        [Display(Name = "Name")]
        [RequiredString]
        [MaxLengthString(100)]
        public string Name { get; set; }

        [Display(Name = "Sort")]
        [NumRange(1, int.MaxValue)]
        public int Sort { get; set; }

        public List<InpSaveEdit_Translate> LstTranslate { get; set; }
    }

    public class InpSaveEdit_Translate
    {
        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }

        [Display(Name = "Title")]
        [RequiredString]
        [MaxLengthString(200)]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
