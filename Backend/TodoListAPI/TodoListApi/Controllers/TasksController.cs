using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoListApi.Application;
using TodoListApi.Application.DTOs;
using TodoListApi.Response;

namespace TodoListApi.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController(ITaskService _taskService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTasks([FromQuery]TaskFilter filter)
        {
            var result = await _taskService.GetTasksAsync(filter);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TodoCreateDto dto)
        {
            var result = await _taskService.CreateTaskAsync(dto);

            return result.ToResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var result  = await _taskService.DeleteTaskAsync(id);

            return result.ToResponse();
        }
    }
}
