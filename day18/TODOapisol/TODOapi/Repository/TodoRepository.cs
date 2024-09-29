using TODOapi.Exceptions;
using TODOapi.Interfaces;
using TODOapi.Models;

namespace TODOapi.Repository
{
    public class TodoRepository : IRepository<int, TodoItem>
    {
        //crud operations done
        // In-memory list of tasks (simulates a database)
         static List<TodoItem> tasks = new List<TodoItem>
        {
            new TodoItem { Id = 1, Task = "Learn MVC", IsCompleted = true },
            new TodoItem { Id = 2, Task = "Build a To-Do list", IsCompleted = false }
        };

        public Task<TodoItem> Add(TodoItem entity)
        {

            throw new NotImplementedException();
        }

        public Task<TodoItem> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public Task<TodoItem> Get(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            if (tasks.Count == 0)
            {
                throw new CollectionEmptyException("tasks");
            }
            return tasks;
            //throw new NotImplementedException();
        }

        public Task<TodoItem> Update(TodoItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
