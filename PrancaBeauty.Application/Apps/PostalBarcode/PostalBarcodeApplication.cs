using Framework.Common.ExMethods;
using Framework.Domain.Enums;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.PostalBarcode;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Domin.PostalBarcodes.PostalBarcodeAgg.Contracts;
using PrancaBeauty.Domin.Users.AddressAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.PostalBarcode
{
    public class PostalBarcodeApplication : IPostalBarcodeApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IPostalBarcodeRepository _PostalBarcodeRepository;

        public PostalBarcodeApplication(ILogger logger, ILocalizer localizer, IServiceProvider serviceProvider, IPostalBarcodeRepository postalBarcodeRepository)
        {
            _Logger=logger;
            _Localizer=localizer;
            _ServiceProvider=serviceProvider;
            _PostalBarcodeRepository=postalBarcodeRepository;
        }

        public async Task<OperationResult> ChangeShipingMethodAsync(InpChangeShipingMethod Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _PostalBarcodeRepository.Get
                                        .Where(a => a.Id==Input.GroupId.ToGuid())
                                        .Where(a => a.BillId==Input.BillId.ToGuid())
                                        .Where(a => a.tblBill.UserId==Input.BuyerUserId.ToGuid())
                                        .Where(a => a.tblBill.Status==BillStatusEnum.NotPayyed)
                                        .SingleOrDefaultAsync();

                if (qData==null)
                    return new OperationResult().Failed("IdNotFound");

                qData.ShippingMethodId=Input.ShippingMethodId.ToGuid();
                await _PostalBarcodeRepository.UpdateAsync(qData, default, true);

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }
    }
}
