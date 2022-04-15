using System;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase
{
    public class OutGetListShowcaseForAdminPage
    {
        public string Id { get; set; }
        public string CountyTitle { get; set; }
        public string Title { get; set; }
        public bool IsFullWidth { get; set; }
        public int Sort { get; set; }
        public bool IsEnable { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
