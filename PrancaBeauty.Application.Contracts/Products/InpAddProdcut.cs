using Framework.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.Products
{
    public class InpAddProdcut
    {
        public string LangId { get; set; }

        public string TopicId { get; set; }

        public string CategoryId { get; set; }

        public string Name { get; set; } // Uniqe Name

        public string Title { get; set; }

        public string Date { get; set; }

        public string Price { get; set; }

        public string MetaTagKeyword { get; set; }

        public string MetaTagCanonical { get; set; }

        public string MetaTagDescreption { get; set; }

        public string Description { get; set; }

        public bool IsDraft { get; set; }

        public List<viAddProduct_Properties> Properties { get; set; }

        public List<viAddProduct_Keywords> Keywords { get; set; }

        public List<viAddProduct_Variants> Variants { get; set; }

        public List<viAddProduct_PostingRestrictions> PostingRestrictions { get; set; }
    }

    public class viAddProduct_Properties
    {

        public string Id { get; set; }

        public string Value { get; set; }
    }

    public class viAddProduct_Keywords
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Similarity { get; set; }
    }

    public class viAddProduct_Variants
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Value { get; set; }

        public string Percent { get; set; }

        public string GuaranteeId { get; set; }

        public string ProductCode { get; set; }

        public ProductVariantItems_SendByEnum SendBy { get; set; } // ارسال توسط: پرنسابیوتی، فروشنده

        public bool IsEnable { get; set; }

        public ProductVariantItems_SendFromEnum SendFrom { get; set; } // ارسال از: 1، 2، 3، 4 رور کاری آینده

        public int CountInStock { get; set; }
    }

    public class viAddProduct_PostingRestrictions
    {
        public string Id { get; set; }

        public string CountryId { get; set; }

        public bool Posting { get; set; }
    }
}
