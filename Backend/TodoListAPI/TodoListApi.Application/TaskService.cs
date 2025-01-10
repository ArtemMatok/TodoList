﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListApi.Application.DTOs;
using TodoListApi.Application.Response;
using TodoListApi.Domain.Entities;

namespace TodoListApi.Application
{
    public interface ITaskService
    {
        Task<PageResultResponse<TodoGetDto>> GetTasksAsync(TaskFilter filter);
        Task<Result<bool>> CreateTaskAsync(TodoCreateDto todoCreateDto);
        Task<Result<bool>> DeleteTaskAsync(int todoId);
    }
    public class TaskService(
        ITaskRepository _taskRepository,
        IMapper _mapper
    ) : ITaskService
    {
        public async Task<Result<bool>> CreateTaskAsync(TodoCreateDto todoCreateDto)
        {
            var todoEntity = _mapper.Map<Todo>(todoCreateDto);

            return await _taskRepository.CreateTaskAsync(todoEntity);
        }

        public async Task<Result<bool>> DeleteTaskAsync(int todoId)
        {
            return await _taskRepository.DeleteTaskAsync(todoId);
        }

        public async Task<PageResultResponse<TodoGetDto>> GetTasksAsync(TaskFilter filter)
        {
            var tasks = await _taskRepository.GetTasksAsync(filter);

            var tasksDto = _mapper.Map<List<TodoGetDto>>(tasks.Items);

            return new PageResultResponse<TodoGetDto>(tasksDto, tasks.TotalCount, filter.PageNumber, filter.PageSize);
        }
    }
}
