namespace Framework.Domain.Enums
{
    public enum BillStatusEnum
    {
        NotPayyed, // پرداخت نشده
        Payyed, // پرداخت شده
        Expired, // منقضی شده: درصورتی که فاکتور بعد از مدت زمانی مشخص، پرداخت نشود به حالت منقضی شده در می آید
    }
}
