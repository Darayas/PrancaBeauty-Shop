using Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Templates.Entitis
{
    public class tblTamplates : IEntity
    {
        public Guid Id { get; set; }
        public string LangId { get; set; }
        public string Name { get; set; }
        public string HtmlCode { get; set; }
    }
}
