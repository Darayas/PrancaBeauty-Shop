using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.BillItems;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Domin.Bills.BillItemsAgg.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.BillItems
{
    public class BillItemsApplication : IBillItemsApplication
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IBillItemsRepository _BillItemsRepository;

        public BillItemsApplication(ILogger logger, IServiceProvider serviceProvider, IBillItemsRepository billItemsRepository, IMapper mapper)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _BillItemsRepository=billItemsRepository;
            _Mapper=mapper;
        }

        public async Task<OperationResult> BillItemFinalPriceRegistrationAsync(InpBillItemFinalPriceRegistration Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _BillItemsRepository.Get
                                        .Where(a => a.Id==Input.Id.ToGuid())
                                        .SingleOrDefaultAsync();

                if (qData==null)
                    return new OperationResult().Failed("IdNotFound");

                qData.TaxPercent=Input.TaxPercent;
                qData.Price=Input.Price;
                qData.TotalPrice=Input.TotalPrice;

                await _BillItemsRepository.UpdateAsync(qData, default, true);

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
