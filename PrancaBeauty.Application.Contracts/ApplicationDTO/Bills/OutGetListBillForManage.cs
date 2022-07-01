using Framework.Domain.Enums;
using System;

namespace PrancaBeauty.Application.Contracts.ApplicationDTO.Bills
{
    public class OutGetListBillForManage
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string UserFullName { get; set; }
        public string GateTitle { get; set; }
        public string Address { get; set; }
        public string BillNumber { get; set; }
        public long TransactionNumber { get; set; }
        public BillStatusEnum Status { get; set; }
    }
}
