using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListApi.Application;
using TodoListApi.Application.Response;
using TodoListApi.Domain.Entities;

namespace TodoListApi.Infrastructure.Data
{
    public class TaskRepository(ApplicationDbContext _context) : ITaskRepository
    {
        public async Task<Result<Todo>> CreateTaskAsync(Todo todo)
        {
            try
            {
                await _context.Todos.AddAsync(todo);
                await _context.SaveChangesAsync();

                return Result<Todo>.Success(todo);
            }
            catch (Exception ex)
            {
                return Result<Todo>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> DeleteTaskAsync(int todoId)
        {
            var taskEntity = await _context.Todos.FindAsync(todoId);
            if (taskEntity is null) return Result<bool>.Failure("Task wasn`t found");

            _context.Todos.Remove(taskEntity);
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }


        public async Task<PageResultResponse<Todo>> GetTasksAsync(TaskFilter filter)
        {
            var tasks = _context.Todos.AsQueryable();

            int totalCount = await tasks.CountAsync();

            tasks = tasks
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);

            var tasksList = await tasks
                .ToListAsync();

            return new PageResultResponse<Todo>(tasksList, totalCount, filter.PageNumber, filter.PageSize);
        }
    }
}
