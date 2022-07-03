using Framework.Application.Consts;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Newtonsoft.Json.Linq;
using PrancaBeauty.Infrastructure.PaymentGates.Contracts;
using ZarinPalWCF;

namespace PrancaBeauty.Infrastructure.PaymentGates.ZarinPal
{
    public class ZarinPalGate : IPayment
    {
        private string _MerchantId;
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;

        public ZarinPalGate(IServiceProvider serviceProvider, ILogger logger, string Data)
        {
            _MerchantId="";
            _ServiceProvider=serviceProvider;
            _Logger=logger;
            _MerchantId= GetInformation(Data);
        }

        public async Task<OutStartPay> StartPaymentAsync(InpStartPay Input)
        {
            try
            {
                Input.CheckModelState(_ServiceProvider);
                var zp = new PaymentGatewayImplementationServicePortTypeClient();

                var _Response = await zp.PaymentRequestAsync(_MerchantId, (int)Input.Amount, Input.Description??"", Input.Email, Input.Mobile, Input.CallBackUrl);

                return new OutStartPay
                {
                    StatusCode=_Response.Body.Status,
                    Authority= _Response.Body.Authority,
                    PaymentyGateUrl= $"https://www.zarinpal.com/pg/StartPay/{_Response.Body.Authority}/ZarinGate"
                };
            }
            catch (ArgumentInvalidException ex)
            {

                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<OutPayVaryfication> PaymentVaryficationAsync(InpPayVaryfication Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region Decode query data
                string _Authority = null;
                {
                    _Authority= Input.QueryData.Where(a => a.Key=="Authority")
                                               .Select(a => a.Value)
                                               .Single();
                }
                #endregion

                var zp = new PaymentGatewayImplementationServicePortTypeClient();
                var _Response = await zp.PaymentVerificationAsync(_MerchantId, _Authority, (int)Input.Amount);

                return new OutPayVaryfication
                {
                    StatusCode= _Response.Body.Status,
                    TransactionNumber=_Response.Body.RefID.ToString()
                };
            }
            catch (ArgumentInvalidException ex)
            {

                _Logger.Debug(ex);
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        private string GetInformation(string Data)
        {
            #region Decrypted data
            string _DecryptedData;
            {
                _DecryptedData= Data.AesDecrypt(AuthConst.SecretKey);
            }
            #endregion

            #region Parse data
            string _MerchantId;
            {
                var _GateData = JArray.Parse(_DecryptedData).Select(a => new
                {
                    MerchantId = (string)a["MerchantId"],
                }).Single();

                _MerchantId=_GateData.MerchantId;
            }
            #endregion

            return _MerchantId;
        }
    }
}
