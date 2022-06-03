using Framework.Infrastructure;
using PrancaBeauty.Domin.PaymentGate.PaymentGateRestrictAgg.Contracts;
using PrancaBeauty.Domin.PaymentGate.PaymentGateRestrictAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.PaymentGateRestricts
{
    public class PaymentGateRestrictRepository : BaseRepository<tblPaymentGateRestricts>, IPaymentGateRestrictRepository
    {
        public PaymentGateRestrictRepository(MainContext Context) : base(Context)
        {

        }
    }
}
