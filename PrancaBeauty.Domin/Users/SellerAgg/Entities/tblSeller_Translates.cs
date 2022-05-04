using Framework.Domain.Contracts;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.Region.LanguagesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Users.SellerAgg.Entities
{
    public class tblSeller_Translates : IEntity
    {
        public Guid Id { get; set; }
        public Guid LangId { get; set; }
        public Guid SellerId { get; set; }
        public Guid? LogoId { get; set; }
        public string Title { get; set; }

        public virtual tblLanguages tblLanguages { get; set; }
        public virtual tblSellers tblSellers { get; set; }
        public virtual tblFiles tblFiles { get; set; }
    }
}
