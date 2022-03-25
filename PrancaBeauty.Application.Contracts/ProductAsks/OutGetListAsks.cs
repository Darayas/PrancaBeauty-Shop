using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ProductAsks
{
    public class OutGetListAsks
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool IsConfirm { get; set; }

        public List<OutGetListAsks_Answer> LstAnswer { get; set; } = new List<OutGetListAsks_Answer>();
    }

    public class OutGetListAsks_Answer
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Text { get; set; }
        public int CountLikes { get; set; }
        public int CountDisLike { get; set; }
        public bool IsConfirm { get; set; }
        public bool IsLike { get; set; }
        public bool IsDisLike { get; set; }
    }
}
