using Framework.Infrastructure;
using PrancaBeauty.Domin.PaymentGate.PaymentGateAgg.Contracts;
using PrancaBeauty.Domin.PaymentGate.PaymentGateAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.PaymentGates
{
    public class PaymentGateRepository : BaseRepository<tblPaymentGates>, IPaymentGateRepository
    {
        public PaymentGateRepository(MainContext Context) : base(Context)
        {

        }
    }
}
