namespace BankManagementApplication.Interfaces.Repositories
{
    public interface IRepository<K, T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity, K key);
        Task<T> Delete(T entity, K key);
        Task<T> Get(K key);
        Task<IEnumerable<T>> GetAll();

    }
}
