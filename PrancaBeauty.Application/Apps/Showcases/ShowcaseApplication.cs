using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Showcase;
using PrancaBeauty.Domin.Showcases.ShowcaseAgg.Contracts;
using PrancaBeauty.Domin.Showcases.ShowcaseAgg.Entities;
using PrancaBeauty.Domin.Sliders.SliderAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Showcases
{
    public class ShowcaseApplication : IShowcaseApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IShowcaseRepository _ShowcaseRepository;

        public ShowcaseApplication(ILogger logger, IServiceProvider serviceProvider, IShowcaseRepository showcaseRepository)
        {
            _Logger=logger;
            _ServiceProvider=serviceProvider;
            _ShowcaseRepository=showcaseRepository;
        }

        public async Task<(OutPagingData, List<OutGetListShowcaseForAdminPage>)> GetListShowcaseForAdminPageAsync(InpGetListShowcaseForAdminPage Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = _ShowcaseRepository.Get
                                               .Select(a => new OutGetListShowcaseForAdminPage
                                               {
                                                   Id=a.Id.ToString(),
                                                   Title=a.tblShowcasesTranslates.Where(b => b.LangId==Input.LangId.ToGuid()).Select(b => b.Title).Single(),
                                                   CountyTitle=a.CountryId!=null ? a.tblCountry.tblCountries_Translates.Where(b => b.LangId==Input.LangId.ToGuid()).Select(b => b.Title).Single() : "",
                                                   IsFullWidth=a.IsFullWidth,
                                                   IsEnable=a.IsEnable,
                                                   IsActive=a.IsActive,
                                                   StartDate=a.StartDate,
                                                   EndDate=a.EndDate,
                                                   Sort=a.Sort
                                               })
                                               .Where(a => Input.Title!=null ? a.Title.Contains(Input.Title) : true)
                                               .OrderBy(a => a.Sort);

                var _PagingData = PagingData.Calc(await qData.LongCountAsync(), Input.Page, Input.Take);
                return (_PagingData, await qData.Skip((int)_PagingData.Skip).Take(_PagingData.Take).ToListAsync());
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Error(ex);
                return default;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return default;
            }
        }

        public async Task<OperationResult> AddShowcaseAsync(InpAddShowcase Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);

                if (Input.EndDate.HasValue)
                    if (Input.StartDate >= Input.EndDate.Value)
                        return new OperationResult().Failed("EndDateMustBeGreaterThanStartDate");
                #endregion

                #region Check Name duplicate
                if (await _ShowcaseRepository.Get.AnyAsync(a => a.Name==Input.Name))
                    return new OperationResult().Failed("NameIsDuplicate");

                #endregion

                #region Get SortNum
                int _Sort = 0;
                {
                    _Sort= await _ShowcaseRepository.Get.CountAsync();
                }
                #endregion

                #region Add Showcase
                {
                    var tShowcase = new tblShowcases
                    {
                        Id= new Guid().SequentialGuid(),
                        Name=Input.Name,
                        CountryId=Input.CountryId!=null ? Input.CountryId.ToGuid() : null,
                        UserId=Input.UserId.ToGuid(),
                        BackgroundColorCode=Input.BackgroundColorCode,
                        CssClass=Input.CssClass,
                        CssStyle=Input.CssStyle,
                        StartDate=Input.StartDate,
                        EndDate=Input.EndDate,
                        IsActive=Input.StartDate<DateTime.Now ? true : false,
                        IsEnable=Input.IsEnable,
                        IsFullWidth=Input.IsFullWidth,
                        Sort=0,
                        tblShowcasesTranslates= Input.LstTranslate.Select(a => new tblShowcasesTranslates
                        {
                            Id= new Guid().SequentialGuid(),
                            LangId=a.LangId.ToGuid(),
                            Title=a.Title,
                            Description=a.Description
                        }).ToList()
                    };

                    await _ShowcaseRepository.AddAsync(tShowcase, default, true);
                }
                #endregion

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Error(ex);
                return default;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return default;
            }
        }
    }
}
