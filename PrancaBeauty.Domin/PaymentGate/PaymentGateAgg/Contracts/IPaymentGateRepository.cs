using Framework.Domain.Contracts;
using PrancaBeauty.Domin.PaymentGate.PaymentGateAgg.Entities;

namespace PrancaBeauty.Domin.PaymentGate.PaymentGateAgg.Contracts
{
    public interface IPaymentGateRepository : IRepository<tblPaymentGates>
    {
    }
}
