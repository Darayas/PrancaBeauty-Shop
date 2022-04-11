using Framework.Domain;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;

namespace PrancaBeauty.Domin.Sliders.SliderAgg.Entities
{
    public class tblSlider : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid FileId { get; set; }
        public string Title { get; set; }
        public int Sort { get; set; }
        public string Url { get; set; }
        public bool IsFollow { get; set; } // Follow, NoFollow

        public bool IsEnable { get; set; }
        public bool IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual tblUsers tblUsers { get; set; }
        public virtual tblFiles tblFiles { get; set; }
    }
}