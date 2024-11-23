namespace ComplaintTicketAPI.Interfaces.InterfaceRepository
{
    public interface IRepository<K, T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity, K key);
        Task<T> Delete(K key);
        Task<T> Get(K key);
        Task<IEnumerable<T>> GetAll();
    }
}
