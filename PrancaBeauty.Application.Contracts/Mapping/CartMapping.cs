using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Cart;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class CartMapping : Profile
    {
        public CartMapping()
        {
            CreateMap<OutGetItemsInCart, vmCompo_CartWidgetMain>();
            CreateMap<OutGetItemsInCartItems, vmCompo_CartWidgetMainItems>();

            CreateMap<OutGetItemsInCart, vmCart>();
            CreateMap<OutGetItemsInCartItems, vmCartItems>();

            CreateMap<viCart, InpChangeQtyCartItem>();
        }
    }
}
