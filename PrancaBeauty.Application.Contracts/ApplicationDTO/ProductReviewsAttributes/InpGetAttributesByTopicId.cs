﻿using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ProductReviewsAttributes
{
    public class InpGetAttributesByTopicId
    {
        [Display(Name= "TopicId")]
        [RequiredString]
        [GUID]
        public string TopicId { get; set; }

        [Display(Name = "LangId")]
        [RequiredString]
        [GUID]
        public string LangId { get; set; }
    }
}
