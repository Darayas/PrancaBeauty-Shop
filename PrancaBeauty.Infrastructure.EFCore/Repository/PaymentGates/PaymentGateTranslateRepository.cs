using Framework.Infrastructure;
using PrancaBeauty.Domin.PaymentGate.PaymentGateAgg.Contracts;
using PrancaBeauty.Domin.PaymentGate.PaymentGateAgg.Entities;
using PrancaBeauty.Infrastructure.EFCore.Context;

namespace PrancaBeauty.Infrastructure.EFCore.Repository.PaymentGates
{
    public class PaymentGateTranslateRepository : BaseRepository<tblPaymentGateTranslate>, IPaymentGateTranslateRepository
    {
        public PaymentGateTranslateRepository(MainContext Context) : base(Context)
        {

        }
    }
}
