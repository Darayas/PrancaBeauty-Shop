using Framework.Common.DataAnnotations.String;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viGetCompo_ProductReviewAttributes
    {
        [Display(Name = "TopicId")]
        [RequiredString]
        [GUID]
        public string TopicId { get; set; }
    }

    
}
