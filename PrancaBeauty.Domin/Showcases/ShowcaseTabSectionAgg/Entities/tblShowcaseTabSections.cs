using Framework.Domain;
using PrancaBeauty.Domin.Showcases.SectionItems.Entitiy;
using PrancaBeauty.Domin.Showcases.ShowcaseTabAgg.Entities;
using System;
using System.Collections.Generic;

namespace PrancaBeauty.Domin.Showcases.ShowcaseTabSectionAgg.Entities
{
    public class tblShowcaseTabSections : IEntity
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public Guid ShowcaseTabId { get; set; }
        public string Name { get; set; }
        public int XlSize { get; set; } // Extra Larg
        public int LgSize { get; set; } // Larg 
        public int MdSize { get; set; } // Medium
        public int SmSize { get; set; } // Smal
        public int XsSize { get; set; } // Extra Small
        public bool IsSlider { get; set; }
        public int CountInSection { get; set; }
        public tblShowcaseTabSectionsHowToDisplayEnum HowToDisplayItems { get; set; }

        public virtual tblShowcaseTabs tblShowcaseTabs { get; set; }
        public virtual tblShowcaseTabSections tblShowcaseTabSectionsParent { get; set; }
        public virtual ICollection<tblShowcaseTabSections> tblShowcaseTabSectionsChild { get; set; }
        public virtual ICollection<tblShowcaseTabSectionItems> tblShowcaseTabSectionItems { get; set; }
    }

    public enum tblShowcaseTabSectionsHowToDisplayEnum
    {
        NoItem = 0, // ایتمی در سکشن قرار نمیگیرد
        FreeItem1 = 1, // تصویر بصورت FullWidth صفحه نمایش داده میشود و عنوان و کد در صورت وجود روی عکس به نمایش در خواهند آمد
        FreeItem2 = 2, // تصویر بزرگ میباشد و عنوان و کد، در صورت وجود زیر عکس نمایش داده میشود
        FreeItem3 = 3, // تصویر بزرگ میباشد و عنوان و کد، در صورت وجود بالای عکس نمایش داده میشود
        FreeItem4 = 4, // تصویر در یک باکس 1/4 در سمت راست و عنوان و کد در یک باکس 3/4 در سمت چپ
        FreeItem5 = 5, // تصویر در یک باکس 1/4 در سمت چپ و عنوان و کد در یک باکس 3/4 در سمت راست
        FreeItem6 = 6, // تصویر در یک باکس 7/12 در سمت راست و عنوان و کد در یک باکس 5/12 در سمت چپ
        FreeItem7 = 7, // تصویر در یک باکس 7/12 در سمت چپ و عنوان و کد در یک باکس 5/12 در سمت راست
        FreeItem8 = 8, // تصویر در یک باکس 6/12 در سمت راست و عنوان و کد در یک باکس 6/12 در سمت چپ
        FreeItem9 = 9, // تصویر در یک باکس 6/12 در سمت چپ و عنوان و کد در یک باکس 6/12 در سمت راست
        DefaultProduct = 10, // حالت معمولی محصول
    }
}
