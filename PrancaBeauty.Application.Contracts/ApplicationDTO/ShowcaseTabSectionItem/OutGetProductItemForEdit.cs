using Framework.Common.DataAnnotations.String;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSectionItem
{
    public class OutGetProductItemForEdit
    {
        public string SectionItemId { get; set; }

        public string ProductId { get; set; }
    }
}
