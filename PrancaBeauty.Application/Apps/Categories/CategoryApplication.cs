using Framework.Application.Services.Security.AntiShell;
using Framework.Common.ExMethods;
using Framework.Common.Utilities.Paging;
using Framework.Exceptions;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PrancaBeauty.Application.Common.FtpWapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Categories;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Common.FtpWapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Domin.Categories.CategoriesAgg.Contracts;
using PrancaBeauty.Domin.Categories.CategoriesAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Categories
{
    public class CategoryApplication : ICategoryApplication
    {
        private readonly ILogger _Logger;
        private readonly ILocalizer _Localizer;
        private readonly IServiceProvider _ServiceProvider;
        private readonly IFtpWapper _FtpWapper;
        private readonly ICategoryRepository _CategoryRepository;
        private readonly ICategory_TranslateRepository _Category_TranslateRepository;

        public CategoryApplication(ICategoryRepository categoryRepository, ILogger logger, IFtpWapper ftpWapper, ICategory_TranslateRepository category_TranslateRepository, ILocalizer localizer, IServiceProvider serviceProvider)
        {
            _CategoryRepository = categoryRepository;
            _Logger = logger;
            _FtpWapper = ftpWapper;
            _Category_TranslateRepository = category_TranslateRepository;
            _Localizer = localizer;
            _ServiceProvider = serviceProvider;
        }

        public async Task<(OutPagingData, List<OutGetCategoryListForAdminPage>)> GetListForAdminPageAsync(InpGetListForAdminPage Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                // آماده سازی اولیه ی کویری
                var qData = _CategoryRepository.Get.Select(a => new OutGetCategoryListForAdminPage
                {
                    Id = a.Id.ToString(),
                    ParentId = a.ParentId.ToString(),
                    Name = a.Name,
                    Title = a.tblCategory_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single(),
                    ImgUrl = a.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                + a.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                + a.tblFiles.tblFilePaths.Path
                                + a.tblFiles.FileName,
                    Sort = a.Sort,
                    ParentTitle = a.tblCategory_Parent.tblCategory_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single(),
                })
                .Where(a => Input.Title != null ? a.Title.Contains(Input.Title) : true)
                .Where(a => Input.ParentTitle != null ? a.ParentTitle.Contains(Input.ParentTitle) : true)
                .OrderBy(a => a.ParentId)
                .ThenBy(a => a.Sort);

                // صفحه بندی داده ها
                var qPagingData = PagingData.Calc(await qData.LongCountAsync(), Input.PageNum, Input.Take);

                return (qPagingData, await qData.Skip((int)qPagingData.Skip).Take(qPagingData.Take).ToListAsync());
            }
            catch (ArgumentInvalidException ex)
            {
                _Logger.Debug(ex);
                return (null, null);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return (null, null);
            }
        }

        public async Task<List<OutGetCategoryListForCombo>> GetListForComboAsync(InpGetListForCombo Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _CategoryRepository.Get
                                                     .Where(a => Input.ParentId != null ? a.ParentId == Guid.Parse(Input.ParentId) /*&& a.Id != Guid.Parse(ParentId)*/ : a.ParentId == null)
                                                     .Select(a => new OutGetCategoryListForCombo
                                                     {
                                                         Id = a.Id.ToString(),
                                                         ParentId = a.ParentId.ToString(),
                                                         Name = a.Name,
                                                         Title = a.tblCategory_Translates.Where(b => b.LangId == Guid.Parse(Input.LangId)).Select(b => b.Title).Single(),
                                                         Sort = a.Sort,
                                                         hasChildren = a.tblCategory_Childs/*.Where(b => b.Id != Guid.Parse(ParentId))*/.Any(),
                                                         ImgUrl = a.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                                                     + a.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                                                     + a.tblFiles.tblFilePaths.Path
                                                                     + a.tblFiles.FileName,
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

        public async Task<OperationResult> AddCategoryAsync(InpAddCategory Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);

                if (Input.LstTranslate == null)
                    throw new ArgumentInvalidException($"{nameof(Input.LstTranslate)} cant be null.");

                if (Input.LstTranslate.Count() == 0)
                    throw new ArgumentInvalidException($"{nameof(Input.LstTranslate)} must be greater than zero.");
                #endregion

                var tCategory = new tblCategoris()
                {
                    Id = new Guid().SequentialGuid(),
                    ParentId = Input.ParentId != null ? Guid.Parse(Input.ParentId) : null,
                    Name = Input.Name,
                    Sort = Input.Sort,
                    tblCategory_Translates=Input.LstTranslate.Select(x => new tblCategory_Translates
                    {
                        Id = new Guid().SequentialGuid(),
                        LangId = Guid.Parse(x.LangId),
                        Title = x.Title,
                        Description = x.Description
                    }).ToList()
                };

                if (Input.Image != null)
                {
                    var _FileUploadResult = await _FtpWapper.UplaodCategoryImgAsync(new InpUplaodCategoryImg { FormFile = Input.Image, UserId = Input.UserId });
                    if (!_FileUploadResult.IsSucceeded)
                        return new OperationResult().Failed(_FileUploadResult.Message);

                    tCategory.ImageId = Guid.Parse(_FileUploadResult.Message);
                }

                await _CategoryRepository.AddAsync(tCategory, default, true);

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<OperationResult> RemoveCategoryAsync(InpRemoveCategory Input)
        {
            try
            {
                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _CategoryRepository.Get
                                                     .Where(a => a.Id == Guid.Parse(Input.Id))
                                                     .Select(a => new
                                                     {
                                                         HasChild = a.tblCategory_Childs.Any(),
                                                         HasProduct = a.tblProducts.Any()
                                                     })
                                                     .SingleOrDefaultAsync();

                if (qData == null)
                    return new OperationResult().Failed("IdNotFound");

                // برسی وجود فرزند
                if (qData.HasChild)
                    return new OperationResult().Failed("CategoryHasChild,CantRemove");

                // برسی وجود محصول عضو دسته جاری
                if (qData.HasProduct)
                    return new OperationResult().Failed("CategoryHasProduct,CantRemove");

                // حذف دسته
                var _Category = await _CategoryRepository.Get.SingleAsync(a => a.Id == Guid.Parse(Input.Id));
                await _CategoryRepository.DeleteAsync(_Category, default);

                // حذف فایل
                if (_Category.ImageId != null)
                    await _FtpWapper.RemoveFileAsync(new InpRemoveFile { FileId = _Category.ImageId.Value.ToString() });

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

        public async Task<OutGetCategoryForEdit> GetForEditAsync(InpGetForEdit Input)
        {
            try
            {

                #region Validation
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _CategoryRepository.Get
                                                    .Where(a => a.Id == Guid.Parse(Input.Id))
                                                    .Select(a => new OutGetCategoryForEdit
                                                    {
                                                        Id = a.Id.ToString(),
                                                        Name = a.Name,
                                                        ParentId = a.ParentId.ToString(),
                                                        Sort = a.Sort,
                                                        ImgCategoryUrl = a.tblFiles.tblFilePaths.tblFileServer.HttpDomin
                                                                            + a.tblFiles.tblFilePaths.tblFileServer.HttpPath
                                                                            + a.tblFiles.tblFilePaths.Path
                                                                            + a.tblFiles.FileName,
                                                        LstTranslate = a.tblCategory_Translates.Select(b => new OutGetForEdit_Translate
                                                        {
                                                            LangId = b.LangId.ToString(),
                                                            Title = b.Title,
                                                            Description = b.Description
                                                        }).ToList()
                                                    })
                                                    .SingleOrDefaultAsync();

                if (qData == null)
                    return null;

                return qData;
            }
            catch (ArgumentInvalidException)
            {
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<OperationResult> SaveEditAsync(InpSaveEdit Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);

                if (Input.LstTranslate == null)
                    throw new ArgumentInvalidException($"{nameof(Input.LstTranslate)} cant be null.");

                if (Input.LstTranslate.Count() == 0)
                    throw new ArgumentInvalidException($"{nameof(Input.LstTranslate)} must be greater than zero.");
                #endregion

                var qData = await _CategoryRepository.Get
                                                     .Where(a => a.Id == Guid.Parse(Input.Id))
                                                     .SingleOrDefaultAsync();

                if (qData == null)
                    return new OperationResult().Failed("IdNotFound");

                qData.Name = Input.Name;
                qData.Sort = Input.Sort;

                if (Input.ParentId != null)
                    qData.ParentId = Guid.Parse(Input.ParentId);
                else
                    qData.ParentId = null;

                if (Input.ParentId?.ToLower() == qData.Id.ToString().ToLower())
                    qData.ParentId = null;

                #region ویرایش ترجمه
                {
                    // حذف کلیه ی ترجمه ها
                    var qTranslates = await _Category_TranslateRepository.Get.Where(a => a.CategoryId == Guid.Parse(Input.Id)).ToListAsync();
                    await _Category_TranslateRepository.DeleteRangeAsync(qTranslates, default);

                    // افزودن ترجمه های جدید
                    await _Category_TranslateRepository.AddRangeAsync(Input.LstTranslate.Select(a => new tblCategory_Translates
                    {
                        Id = new Guid().SequentialGuid(),
                        CategoryId = Guid.Parse(Input.Id),
                        Description = a.Description,
                        LangId = Guid.Parse(a.LangId),
                        Title = a.Title
                    }).AsEnumerable(), default);
                }
                #endregion

                #region ویرایش تصویر
                if (Input.Image != null)
                {
                    if (qData.ImageId != null)
                    {
                        // حذف تصویر قبلی
                        await _FtpWapper.RemoveFileAsync(new InpRemoveFile { FileId = qData.ImageId.Value.ToString() });
                        qData.tblFiles = null;
                    }

                    // اپلود تصویر جدید
                    var _FileUploadResult = await _FtpWapper.UplaodCategoryImgAsync(new InpUplaodCategoryImg { FormFile = Input.Image, UserId = Input.UserId });
                    if (_FileUploadResult.IsSucceeded == false)
                        return new OperationResult().Failed(_FileUploadResult.Message);

                    qData.ImageId = Guid.Parse(_FileUploadResult.Message);
                }
                #endregion

                await _CategoryRepository.UpdateAsync(qData, default);

                return new OperationResult().Succeeded();
            }
            catch (ArgumentInvalidException ex)
            {
                return new OperationResult().Failed(ex.Message);
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return new OperationResult().Failed("Error500");
            }
        }

        public async Task<IEnumerable<OutGetParentsByChildId>> GetParentsByChildIdAsync(InpGetParentsByChildId Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                Guid? ParentId = Guid.Parse(Input.ChildId);
                Stack<OutGetParentsByChildId> StkItems = new Stack<OutGetParentsByChildId>();

                while (ParentId != null)
                {
                    var qData = await _CategoryRepository.Get
                                                         .Where(a => a.Id == ParentId.Value)
                                                         .Include(a => a.tblCategory_Parent)
                                                         .Include(a => a.tblCategory_Translates)
                                                         .SingleOrDefaultAsync();
                    if (qData == null)
                        break;

                    StkItems.Push(new OutGetParentsByChildId()
                    {
                        Id = qData.Id.ToString(),
                        Name = qData.Name,
                        Title = qData.tblCategory_Translates.Where(a => a.LangId == Guid.Parse(Input.LangId)).Select(a => a.Title).Single()
                    });

                    ParentId = qData.ParentId;
                }

                return StkItems.ToList();
            }
            catch (ArgumentInvalidException)
            {
                return null;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        public async Task<OutGetCategoryDetailsByName> GetCategoryDetailsByNameAsync(InpGetCategoryDetailsByName Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _CategoryRepository.Get
                                        .Where(a => a.Name==Input.Name)
                                        .Select(a => new OutGetCategoryDetailsByName
                                        {
                                            Id=a.Id.ToString(),
                                            Title=a.tblCategory_Translates.Where(b => b.LangId==Input.LangId.ToGuid()).Select(b => b.Title).Single(),
                                            Descreption=a.tblCategory_Translates.Where(b => b.LangId==Input.LangId.ToGuid()).Select(b => b.Description).Single()
                                        })
                                        .SingleOrDefaultAsync();

                if (qData==null)
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

        public async Task<List<OutGetCategoriesForSeachAutoComplete>> GetCategoriesForSeachAutoCompleteAsync(InpGetCategoriesForSeachAutoComplete Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                var qData = await _CategoryRepository.Get
                                            .Where(a => a.ParentId != null)
                                            .Where(a => a.tblCategory_Childs.Any() == false)
                                            .OrderByDescending(a => a.tblProducts.Where(a=>a.IsConfirmed && !a.IsDelete && !a.IsDraft).Count())
                                            .Select(a => new OutGetCategoriesForSeachAutoComplete
                                            {
                                                Id=a.Id.ToString(),
                                                Name=a.Name,
                                                Title=a.tblCategory_Translates.Where(b => b.LangId==default).Select(b => b.Title).Single(),
                                                ParentTitle=a.tblCategory_Parent.tblCategory_Translates.Where(b => b.LangId==default).Select(b => b.Title).Single(),
                                            })
                                            .Where(a => a.Title.Contains(Input.Title))
                                            .Take(3)
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
