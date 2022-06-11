using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.TaxGroup
{
    public class InpGetListTaxGroupForCombo
    {
        [Display(Name = "CountryId")]
        [RequiredString]
        [GUID]
        public string CountryId { get; set; }

        [Display(Name = "Text")]
        [MaxLengthString(100)]
        public string Text { get; set; }
    }
}
