using Framework.Domain;
using PrancaBeauty.Domin.Users.UserAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Users.AccessLevelAgg.Entities
{
    public class tblAccessLevels : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        public virtual ICollection<tblUsers> tblUsers{ get; set; }
    }
}
