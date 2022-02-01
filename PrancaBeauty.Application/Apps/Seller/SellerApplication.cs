using Framework.Application.Consts;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Contracts.Sellers;
using PrancaBeauty.Domin.Users.SellerAgg.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Seller
{
    public class SellerApplication : ISellerApplication
    {
        private readonly ILogger _Logger;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ISellersRepository _SellersRepository;

        public SellerApplication(ISellersRepository sellersRepository, ILogger logger, IServiceProvider serviceProvider)
        {
            _SellersRepository = sellersRepository;
            _Logger = logger;
            _ServiceProvider = serviceProvider;
        }

        public async Task<List<OutGetListSellerForCombo>> GetListSellerForComboAsync(InpGetListSellerForCombo Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _SellersRepository.Get
                                                    .Select(a => new OutGetListSellerForCombo
                                                    {
                                                        Id = a.Id.ToString(),
                                                        Name = a.Name,
                                                        Title = a.tblSeller_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single(),
                                                        LogoUrl = a.tblSeller_Translates.Where(a => a.LangId == Guid.Parse(Input.LangId)).Select(a => a.LogoId) != null ?
                                                                                            (a.tblSeller_Translates.Where(a => a.LangId == Guid.Parse(Input.LangId))
                                                                                                                   .Select(a => new
                                                                                                                   {
                                                                                                                       Url = a.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                                                                                                             + a.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                                                                                                             + a.tblFiles.tblFilePaths.Path
                                                                                                                             + a.tblFiles.FileName
                                                                                                                   })).Single().Url
                                                                                    : PublicConst.DefaultSellerLogoImg
                                                    })
                                                    .Where(a => Input.SellerTitle != null ? (a.Name.Contains(Input.SellerTitle)) || (a.Title.Contains(Input.SellerTitle)) : true)
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

        public async Task<string> GetSellerIdByUserIdAsync(InpGetSellerIdByUserId Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                return await _SellersRepository.Get
                                               .Where(a => a.UserId == Guid.Parse(Input.UserId))
                                               .Select(a => a.Id.ToString())
                                               .SingleOrDefaultAsync();
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

        public async Task<OutGetSummaryBySellerId> GetSummaryBySellerIdAsync(InpGetSummaryBySellerId Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _SellersRepository.Get
                                                    .Where(a => a.Id == Guid.Parse(Input.SellerId))
                                                    .Select(a => new OutGetSummaryBySellerId
                                                    {
                                                        Id = a.Id.ToString(),
                                                        FulUserName = a.tblUsers.FirstName + " " + a.tblUsers.LastName,
                                                        DateTime = a.Date,
                                                        SellerTitle = a.tblSeller_Translates.Where(a => a.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single(),
                                                        SellerLogoUrl = a.tblSeller_Translates.Where(a => a.LangId == Guid.Parse(Input.LangId)).Select(b => new
                                                        {
                                                            LogoUrl = b.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                                                        + b.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                                                        + b.tblFiles.tblFilePaths.Path
                                                                        + b.tblFiles.FileName
                                                        }).Single().LogoUrl,
                                                        IsSellerConfimed = a.IsConfirm
                                                    })
                                                    .SingleOrDefaultAsync();

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
