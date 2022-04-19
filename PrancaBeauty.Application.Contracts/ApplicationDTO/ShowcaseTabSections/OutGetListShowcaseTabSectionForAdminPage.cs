namespace PrancaBeauty.Application.Contracts.ApplicationDTO.ShowcaseTabSections
{
    public class OutGetListShowcaseTabSectionForAdminPage
    {
        public string Id { get; set; }

        public string ParentName { get; set; }

        public string Name { get; set; }

        public int XlSize { get; set; } // Extra Larg

        public int LgSize { get; set; } // Larg 

        public int MdSize { get; set; } // Medium

        public int SmSize { get; set; } // Smal

        public int XsSize { get; set; } // Extra Small

        public bool IsSlider { get; set; }

        public int CountInSection { get; set; }
    }
}
