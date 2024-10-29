namespace Claims_AssignmentApplication.Interfaces
{
   public interface IRepository< K,T> where T : class
    {
        Task<T> GetByIdAsync(K id); // Get an entity by its ID
        Task<IEnumerable<T>> GetAllAsync(); // Get all entities
        Task<T> AddAsync(T entity); // Add a new entity
        Task<bool> DeleteAsync(K id); // Delete an entity by its ID (returns true if successful)
        Task UpdateAsync(T entity); // Update an existing entity
    }
   
}
