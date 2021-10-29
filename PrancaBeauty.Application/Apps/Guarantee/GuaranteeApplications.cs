using PrancaBeauty.Domin.Product.GuaranteeAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Guarantee
{
    public class GuaranteeApplications : IGuaranteeApplications
    {
        private readonly IGuaranteeRepository _GuaranteeRepository;

        public GuaranteeApplications(IGuaranteeRepository guaranteeRepository)
        {
            _GuaranteeRepository = guaranteeRepository;
        }
    }

}
