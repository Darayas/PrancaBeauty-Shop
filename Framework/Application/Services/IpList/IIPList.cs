namespace Framework.Application.Services.IpList
{
    public interface IIPList
    {
        void AddRange(string fromIP, string toIP);
        bool CheckNumber(string ipNumber);
    }
}