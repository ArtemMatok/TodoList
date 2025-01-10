using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListApi.Application;
using TodoListApi.Application.DTOs;
using TodoListApi.Application.Response;
using TodoListApi.Domain.Entities;

namespace TodoListApi.Tests.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _mapperMock = new Mock<IMapper>();
            _taskService = new TaskService(_taskRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateTaskAsync_ShouldReturnTrue_WhenTaskCreatedSuccessfully()
        {
            var todoCreateDto = new TodoCreateDto("Test Task");
            var todoEntity = new Todo { Title = "Test Task" };

            _mapperMock.Setup(m => m.Map<Todo>(todoCreateDto)).Returns(todoEntity);
            _taskRepositoryMock.Setup(r => r.CreateTaskAsync(todoEntity))
                .ReturnsAsync(Result<bool>.Success(true));

            var result = await _taskService.CreateTaskAsync(todoCreateDto);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeTrue();
        }

        [Fact]
        public async Task CreateTaskAsync_ShouldReturnFailure_WhenRepositoryFails()
        {
            var todoCreateDto = new TodoCreateDto("Test Task");
            var todoEntity = new Todo { Title = "Test Task" };

            _mapperMock.Setup(m => m.Map<Todo>(todoCreateDto)).Returns(todoEntity);
            _taskRepositoryMock.Setup(r => r.CreateTaskAsync(todoEntity))
                .ReturnsAsync(Result<bool>.Failure("Failed to create task"));

            var result = await _taskService.CreateTaskAsync(todoCreateDto);

            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Failed to create task");
        }

        [Fact]
        public async Task DeleteTaskAsync_ShouldReturnTrue_WhenTaskDeletedSuccessfully()
        {
            var todoId = 1;

            _taskRepositoryMock.Setup(r => r.DeleteTaskAsync(todoId))
                .ReturnsAsync(Result<bool>.Success(true));

            var result = await _taskService.DeleteTaskAsync(todoId);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteTaskAsync_ShouldReturnFailure_WhenTaskDoesNotExist()
        {
            var todoId = 99;

            _taskRepositoryMock.Setup(r => r.DeleteTaskAsync(todoId))
                .ReturnsAsync(Result<bool>.Failure("Task not found"));

            var result = await _taskService.DeleteTaskAsync(todoId);

            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Task not found");
        }

        [Fact]
        public async Task GetTasksAsync_ShouldReturnPagedResponse_WhenTasksExist()
        {
            var filter = new TaskFilter { PageNumber = 1, PageSize = 2 };
            var tasks = new PageResultResponse<Todo>(new List<Todo>
            {
                new Todo { TodoId = 1, Title = "Task 1" },
                new Todo { TodoId = 2, Title = "Task 2" }
            }, 2, filter.PageNumber, filter.PageSize);

            var tasksDto = new List<TodoGetDto>
            {
                new TodoGetDto(1,"Task 1"),
                new TodoGetDto(2,"Task 2"),
            };

            _taskRepositoryMock.Setup(r => r.GetTasksAsync(filter))
                .ReturnsAsync(tasks);
            _mapperMock.Setup(m => m.Map<List<TodoGetDto>>(tasks.Items))
                .Returns(tasksDto);

            var result = await _taskService.GetTasksAsync(filter);

            result.Items.Should().HaveCount(2);
            result.Items.Should().BeEquivalentTo(tasksDto);
            result.TotalCount.Should().Be(2);
            result.CurrentPage.Should().Be(filter.PageNumber);
            result.PageSize.Should().Be(filter.PageSize);
        }
    }
}
