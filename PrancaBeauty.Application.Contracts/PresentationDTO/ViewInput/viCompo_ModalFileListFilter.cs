using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_ModalFileListFilter
    {
        public string FieldName { get; set; }
        public string FileTitle { get; set; }
        public string FileTypeId { get; set; }
        public string UploaderUserId { get; set; }
        public int? Sort { get; set; }
    }
}
