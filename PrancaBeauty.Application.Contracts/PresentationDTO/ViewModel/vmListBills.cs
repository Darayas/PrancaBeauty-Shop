using Framework.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewModel
{
    public class vmListBills
    {
        public string Id { get; set; }

        public string Date { get; set; }
        public string UserFullName { get; set; }
        public string GateTitle { get; set; }
        public string Address { get; set; }

        [Display(Name = "BillNumber")]
        public string BillNumber { get; set; }

        public string TransactionNumber { get; set; }

        [Display(Name = "Status")]
        public BillStatusEnum Status { get; set; }
    }
}
