namespace Framework.Domain.Enums
{
    public enum PostalBarcodeEnum
    {
        InCheckingOut, // درحال برسی
        InPacking, // درحال بسته بندی
        DeliveryToThePost, // تحویل پست داده شده
        CarryingToDestination, // در حال حمل به مقصد
        DeliveryToTheRecipient // تحویل به گیرنده
    }
}
