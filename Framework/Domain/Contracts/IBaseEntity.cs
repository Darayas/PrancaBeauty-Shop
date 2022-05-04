namespace Framework.Domain.Contracts
{
    public interface IBaseEntity<T>
    {
        public T Id { get; set; }
    }
}
