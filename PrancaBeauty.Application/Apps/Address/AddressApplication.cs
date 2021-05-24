using PrancaBeauty.Domin.Users.AddressAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Address
{
    public class AddressApplication: IAddressApplication
    {
        private readonly IAddressRepository _AddressRepository;
        public AddressApplication(IAddressRepository addressRepository)
        {
            _AddressRepository = addressRepository;
        }
    }
}
