using System.Collections.Generic;

namespace PrancaBeauty.WebApp.Models.ViewModel
{
    public class vmCompo_ListProductAsks
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool IsConfirm { get; set; }

        public List<vmGetListAsks_Answer> LstAnswer { get; set; } = new List<vmGetListAsks_Answer>();
    }

    public class vmGetListAsks_Answer
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Text { get; set; }
        public int CountLikes { get; set; }
        public int CountDisLike { get; set; }
        public bool IsConfirm { get; set; }
    }
}
