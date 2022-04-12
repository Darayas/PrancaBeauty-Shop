using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viListSliderSorting
    {
        [Display(Name ="Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }

        [Display(Name = "Action")]
        [RequiredString]
        public viListSliderSortingItem Act { get; set; }
    }

    public enum viListSliderSortingItem
    {
        Up=0,
        Down=1
    }
}
