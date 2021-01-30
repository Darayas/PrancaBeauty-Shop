using Framework.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrancaBeauty.Domin.Users.UserAgg.Entities
{
    public class tblUsers : IdentityUser<Guid>, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Date { get; set; }

    }
}
