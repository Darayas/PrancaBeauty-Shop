﻿using Framework.Domain;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Users.SellerAgg.Entities
{
    public class tblSellers : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public virtual tblUsers tblUsers { get; set; }
        public virtual ICollection<tblSeller_Translates> tblSeller_Translates { get; set; }

    }
}
