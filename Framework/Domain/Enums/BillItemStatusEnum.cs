namespace Framework.Domain.Enums
{
    public enum BillItemStatusEnum
    {
        None,
        RequestToReturn, // درخواست عودت محصول
        ConfirmedToReturn, // قبول عودت محصول از طرف سایت
        RejectToReturn // رد درخواست عودت
    }
}
