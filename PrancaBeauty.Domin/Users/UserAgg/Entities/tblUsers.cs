using Framework.Domain;
using Microsoft.AspNetCore.Identity;
using PrancaBeauty.Domin.FileServer.FileAgg.Entities;
using PrancaBeauty.Domin.Users.AccessLevelAgg.Entities;
using PrancaBeauty.Domin.Users.AddressAgg.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrancaBeauty.Domin.Users.UserAgg.Entities
{
    public class tblUsers : IdentityUser<Guid>, IEntity
    {
        public Guid AccessLevelId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime Date { get; set; }
        public string PasswordPhoneNumber { get; set; }
        public DateTime? LastTrySentSms { get; set; }
        public bool IsActive { get; set; }

        public virtual tblAccessLevels tblAccessLevels { get; set; }
        public virtual ICollection<tblFiles> tblFiles { get; set; }
        public virtual ICollection<tblAddress> tblAddress { get; set; }

    }
}
