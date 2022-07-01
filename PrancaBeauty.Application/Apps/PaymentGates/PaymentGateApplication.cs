using AutoMapper;
using Framework.Application.Consts;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.FileServer;
using PrancaBeauty.Application.Contracts.ApplicationDTO.PaymentGate;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Domin.PaymentGate.PaymentGateAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
                                        .Where(a => a.IsEnable)
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

        public async Task<OperationResult<OutGetGateData>> GetGateDataAsync(InpGetGateData Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _PaymentGateRepository.Get
                                        .Where(a => a.Name==Input.GateName)
                                        .Select(a => new OutGetGateData
                                        {
                                            IsEnable = a.IsEnable,
                                            EncryptedData = a.Data
                                        })
                                        .SingleOrDefaultAsync();

                if (qData==null)
                    return new OperationResult<OutGetGateData>().Failed("GateNotFound");

                if (qData.IsEnable==false)
                    return new OperationResult<OutGetGateData>().Failed("GateIsDisable"); ;

                return new OperationResult<OutGetGateData>().Succeeded(qData);
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return new OperationResult<OutGetGateData>().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult<OutGetGateData>().Failed("Error500");
            }
        }
    }
}
