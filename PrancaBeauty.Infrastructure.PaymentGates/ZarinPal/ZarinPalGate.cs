using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using PrancaBeauty.Infrastructure.PaymentGates.ZarinPal.Contracts;
using ZarinPalWCF;

namespace PrancaBeauty.Infrastructure.PaymentGates.ZarinPal
{
    public class ZarinPalGate : IPayment
    {
        private readonly string _MerchantId;
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        public ZarinPalGate(IServiceProvider serviceProvider, ILogger logger)
        {
            _MerchantId="";
            _ServiceProvider=serviceProvider;
            _Logger=logger;
        }

        public async Task<OutZpStartPayment> StartPaymentAsync(InpZpStartPayment Input)
        {
            try
            {
                Input.CheckModelState(_ServiceProvider);
                var zp = new PaymentGatewayImplementationServicePortTypeClient();

                var _Response = await zp.PaymentRequestAsync(_MerchantId, (int)Input.Amount, Input.Description??"", Input.Email, Input.Mobile, Input.CallBackUrl);

                return new OutZpStartPayment
                {
                    StatusCode=_Response.Body.Status,
                    Authority= _Response.Body.Authority,
                    PaymentyGateUrl= $"https://www.zarinpal.com/pg/StartPay/{_Response.Body.Authority}/ZarinGate"
                };
            }
            catch (ArgumentInvalidException)
            {

                // Log
                return null;
            }
            catch (Exception)
            {
                // Log
                return null;
            }
        }

        public async Task<OutZpPaymentVaryfication> PaymentVaryficationAsync(InpZpPaymentVaryfication Input)
        {
            try
            {
                Input.CheckModelState(_ServiceProvider);

                var zp = new PaymentGatewayImplementationServicePortTypeClient();
                var _Response = await zp.PaymentVerificationAsync(_MerchantId, Input.Authority, (int)Input.Amount);

                return new OutZpPaymentVaryfication
                {
                    StatusCode= _Response.Body.Status,
                    TransactionNumber=_Response.Body.RefID.ToString()
                };
            }
            catch (ArgumentInvalidException)
            {

                // Log
                return null;
            }
            catch (Exception)
            {
                // Log
                return null;
            }
        }
    }
}
