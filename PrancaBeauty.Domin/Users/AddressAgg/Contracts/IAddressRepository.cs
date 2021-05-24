using Framework.Domain;
using PrancaBeauty.Domin.Users.AddressAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Domin.Users.AddressAgg.Contracts
{
    public interface IAddressRepository : IRepository<tblAddress>
    {
    }
}
