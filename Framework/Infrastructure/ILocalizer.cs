namespace Framework.Infrastructure
{
    public interface ILocalizer
    {
        public string this[string Name] { get; }
        public string this[string Name, params object[] argumets] { get; }
    }
}