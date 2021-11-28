﻿using Framework.Common.DataAnnotations.File;
using Framework.Common.DataAnnotations.String;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_Combo_Province
    {
        [RequiredString]
        [GUID]
        public string CountryId { get; set; }

        [GUID]
        public string ProvinceId { get; set; }
        public string FieldName { get; set; }
    }
}
