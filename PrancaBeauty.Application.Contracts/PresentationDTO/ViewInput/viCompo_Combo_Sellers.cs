namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_Combo_Sellers
    {
        public string SellerId { get; set; }
        public string LangId { get; set; }
        public string FieldName { get; set; }
        public bool ShowLabale { get; set; }
        public bool IsEnable { get; set; } = true;
    }
}
