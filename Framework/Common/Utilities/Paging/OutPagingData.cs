using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.Utilities.Paging
{
    public class OutPagingData
    {
        public long CountAllItem { get; set; }
        public int CountAllPage { get; set; }
        public int Page { get; set; }
        public int Take { get; set; }
        public long Skip { get; set; }
    }
}
