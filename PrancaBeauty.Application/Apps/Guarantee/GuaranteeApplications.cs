﻿using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.Guarantee;
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
        private readonly ILogger _Logger;
        private readonly IGuaranteeRepository _GuaranteeRepository;

        public GuaranteeApplications(IGuaranteeRepository guaranteeRepository, ILogger logger)
        {
            _GuaranteeRepository = guaranteeRepository;
            _Logger = logger;
        }

        public async Task<List<OutGetListForCombo>> GetListForComboAsync(InpGetListForCombo Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState();
                #endregion

                var qData = await _GuaranteeRepository.Get
                                                     .Where(a => a.IsEnable)
                                                     .Select(a => new OutGetListForCombo()
                                                     {
                                                         Id = a.Id.ToString(),
                                                         Name = a.Name,
                                                         Title = a.tblGuarantee_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single()
                                                     })
                                                     .Where(a => Input. Search != null ? a.Title.Contains(Input.Search) || a.Name.Contains(Input.Search) : true)
                                                     .OrderBy(a => a.Title)
                                                     .Skip(0)
                                                     .Take(40)
                                                     .ToListAsync();

                if (qData == null)
                    return null;

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
