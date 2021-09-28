using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.WebApp.Models.ViewInput
{
    public class viCompo_ModalFileListFilter
    {
        public string Title { get; set; }
        public string FileTypeId { get; set; }
        public string UserId { get; set; }
        public int? Sort { get; set; }
    }
}
