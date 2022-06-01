using AutoMapper;
using Framework.Common.ExMethods;
using Framework.Exceptions;
using Framework.Infrastructure;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Cart;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Results;
using PrancaBeauty.Domin.Cart.CartAgg.Contracts;
using System;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Apps.Carts
{
    public class CartApplication : ICartApplication
    {
        private readonly ILogger _Logger;
        private readonly IMapper _Mapper;
        private readonly IServiceProvider _ServiceProvider;
        private readonly ICartRepository _CartRepository;

        public CartApplication(ILogger logger, IMapper mapper, IServiceProvider serviceProvider, ICartRepository cartRepository)
        {
            _Logger=logger;
            _Mapper=mapper;
            _ServiceProvider=serviceProvider;
            _CartRepository=cartRepository;
        }

        public async Task<OperationResult> AddToCartAsync(InpAddToCart Input)
        {
            try
            {
                #region Validations
                Input.CheckModelState(_ServiceProvider);
                #endregion

                #region برسی موجود نبودن محصول و نوع جاری در سبد خرید کاربر

                #endregion

                #region افزودن محصول و نوع به سبد خرید کاربر

                #endregion

                return new OperationResult().Succeeded("");
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
