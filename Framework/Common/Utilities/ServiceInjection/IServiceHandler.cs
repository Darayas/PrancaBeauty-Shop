namespace Framework.Common.Utilities.ServiceInjection
{
    public interface IServiceHandler
    {
        T GetService<T>();
    }
}