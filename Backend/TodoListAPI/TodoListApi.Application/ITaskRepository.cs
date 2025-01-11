using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListApi.Application.Response;
using TodoListApi.Domain.Entities;

namespace TodoListApi.Application
{
    public interface ITaskRepository
    {
        Task<PageResultResponse<Todo>> GetTasksAsync(TaskFilter filter);
        Task<Result<Todo>> CreateTaskAsync(Todo todo);
        Task<Result<bool>> DeleteTaskAsync(int todoId);
    }
}
