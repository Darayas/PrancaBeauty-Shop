using Framework.Infrastructure;
using PrancaBeauty.Domin.Users.AddressAgg.Contracts;
using PrancaBeauty.Domin.Users.AddressAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.Address
{
    public class AddressRepository : BaseRepository<tblAddress>, IAddressRepository
    {
        public AddressRepository(MainContext context) : base(context)
        {

        }
    }
}
