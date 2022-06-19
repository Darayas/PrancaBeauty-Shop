using AutoMapper;
using PrancaBeauty.Application.Contracts.ApplicationDTO.Bills;
using PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class BillMapping : Profile
    {
        public BillMapping()
        {
            CreateMap<OutGetBillDetails, vmBill>();
            CreateMap<OutGetBillDetailsItemGroups, vmBillItemGroups>();
            CreateMap<OutGetBillDetailsItems, vmBillItems>();
            CreateMap<OutGetListBillForManage, vmListBills>();
        }
    }
}
