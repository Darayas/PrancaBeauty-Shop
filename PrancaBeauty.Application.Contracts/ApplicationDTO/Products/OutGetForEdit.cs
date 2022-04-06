using Framework.Common.DataAnnotations.Numbers.All;
using Framework.Common.DataAnnotations.String;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Products
{
    public class OutGetForEdit
    {
        public string Id { get; set; }

        public string AuthorUserId { get; set; }

        public string LangId { get; set; }

        public string TopicId { get; set; }

        public string CategoryId { get; set; }

        public string Name { get; set; } // Uniqe Name

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public double Price { get; set; }

        public string MetaTagKeyword { get; set; }

        public string MetaTagCanonical { get; set; }

        public string MetaTagDescreption { get; set; }

        public string Description { get; set; }

        public bool IsDraft { get; set; }

        public string ProductImagesId { get; set; }
    }
}
