using TODOapi.Models;

namespace TODOapi.Interfaces
{
    public interface ITodoService
    {
        public Task<IEnumerable<TodoItem>> ViewTask();

    }
}
