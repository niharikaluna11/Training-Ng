using TODOapi.Interfaces;
using TODOapi.Models;

namespace TODOapi.Services
{
    // services are defined here
    //view task, update create delete
    public class TodoService :ITodoService
    {

       
        private readonly IRepository<int, TodoItem> _todoRepository;

        public TodoService(IRepository<int, TodoItem> todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<IEnumerable<TodoItem>> ViewTask()
        {
            var tasks = (await _todoRepository.GetAll()).ToList().OrderBy(p => p.Task);
              return tasks;
        }
    }
}
