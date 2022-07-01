using Framework.Common.ExMethods;
using Framework.Exceptions;
using PrancaBeauty.Infrastructure.PaymentGates.ZarinPal.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZarinPalWCF;

namespace PrancaBeauty.Infrastructure.PaymentGates.ZarinPal
{
    public class ZarinPalGate : IPayment
    {
        private readonly string _MerchantId;
        private readonly IServiceProvider _ServiceProvider;
        public ZarinPalGate(IServiceProvider serviceProvider)
        {
            _MerchantId="";
            _ServiceProvider=serviceProvider;
        }

        public async Task<OutStartPayment> StartPaymentAsync(InpStartPayment Input)
        {
            try
            {
                Input.CheckModelState(_ServiceProvider);
                var zp = new PaymentGatewayImplementationServicePortTypeClient();

                var _Response = await zp.PaymentRequestAsync(_MerchantId, (int)Input.Amount, Input.Description??"", Input.Email, Input.Mobile, Input.CallBackUrl);

            }
            catch (ArgumentInvalidException ex)
            {

                // Log
                return null;
            }
            catch (Exception ex)
            {
                // Log
                return null;
            }
        }

        public async Task<OutPaymentVaryfication> PaymentVaryficationAsync(InpPaymentVaryfication Input)
        {
            return default;
        }
    }
}
