using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.PaymentGate;
using PrancaBeauty.Domin.PaymentGate.PaymentGateAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.PaymentGates
{
    public class PaymentGateApplication : IPaymentGateApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IMapper _Mapper;
        private readonly IPaymentGateRepository _PaymentGateRepository;
        public PaymentGateApplication(ILogger logger, IServiceProvider serviceProvider, IMapper mapper, IPaymentGateRepository paymentGateRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _Mapper=mapper;
            _PaymentGateRepository=paymentGateRepository;
        }

        public async Task<List<OutGetPaymentGateByCountry>> GetPaymentGateByCountryAsync(InpGetPaymentGateByCountry Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _PaymentGateRepository.Get
                                        .Where(a => a.tblPaymentGateRestricts.Any(b => b.CountryId==Input.CountryId.ToGuid() && b.CurrencyId==Input.CurrencyId.ToGuid()))
                                        .Select(a => new OutGetPaymentGateByCountry
                                        {
                                            Id=a.Id.ToString(),
                                            Title=a.tblPaymentGateTranslate.Where(b => b.LangId==Input.LangId.ToGuid()).Select(b => b.Title).Single(),
                                            ImgUrl=a.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                                    + a.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                                    + a.tblFiles.tblFilePaths.Path
                                                    + a.tblFiles.FileName
                                        })
                                        .ToListAsync();

                return qData;
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
    }
}
