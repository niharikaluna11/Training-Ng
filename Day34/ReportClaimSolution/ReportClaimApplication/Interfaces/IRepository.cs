using System.Linq.Expressions;

namespace ReportClaimApplication.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id); // Get an entity by its ID
        Task<IEnumerable<T>> GetAllAsync(); // Get all entities
        Task<T> AddAsync(T entity); // Add a new entity
        Task<T> DeleteAsync(int id); // Delete an entity by its ID
        Task UpdateAsync(T entity); // Update an existing entity
    }
}
