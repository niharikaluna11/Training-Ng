using Microsoft.AspNetCore.Mvc;
using TODOapi.Models;
using TODOapi.Interfaces;
using TODOapi.Exceptions;

namespace TODOapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {

    
        // service is defined
        private readonly ITodoService _todoService;

        public TodoController(ITodoService service)
        {
            _todoService = service;
        }
       
        [HttpGet]
        //display the task
        public async Task<IActionResult> ViewTask()
        {
            try
            {
                var task = await _todoService.ViewTask();
                return Ok(task);
            }
            catch (CollectionEmptyException e)
            {

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                //return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                return BadRequest(e.Message);
            }
        }

       

        /*// Add a new task
        [HttpPost]
        public IActionResult Create(string task)
        {
            if (!string.IsNullOrEmpty(task))
            {
                var newTask = new TodoItem
                {
                    Id = _tasks.Count + 1,
                    Task = task,
                    IsCompleted = false
                };
                _tasks.Add(newTask);
            }
            return RedirectToAction("Index");
        }

        // Mark a task as completed
        public IActionResult MarkComplete(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
            }
            return RedirectToAction("Index");
        }*/
    }
}
