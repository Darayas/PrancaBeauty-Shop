using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase
{
    public class InpRemoveShowcase
    {
        [Display(Name = "Id")]
        [RequiredString]
        [GUID]
        public string Id { get; set; }
    }
}
