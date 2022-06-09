using AutoMapper;
using Framework.Infrastructure;
using PrancaBeauty.Domin.Bills.BillItemsAgg.Contracts;
using System;

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
    }
}
