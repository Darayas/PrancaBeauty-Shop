﻿using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_PropertisSearch
    {

        [Display(Name = "TopicId")]
        [RequiredString]
        [GUID]
        public string TopicId { get; set; }

        [Display(Name = "SelectedValues")]
        [MaxLengthString(1000)]
        public string SelectedValues { get; set; }
    }
}
