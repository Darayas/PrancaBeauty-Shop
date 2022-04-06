using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_Combo_Cities
    {
        [RequiredString]
        [GUID]
        public string ProvinceId { get; set; }

        [GUID]
        public string CityId { get; set; }
        public string FieldName { get; set; }
    }
}
