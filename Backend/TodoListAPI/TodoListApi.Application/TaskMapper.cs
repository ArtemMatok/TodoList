using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListApi.Application.DTOs;
using TodoListApi.Domain.Entities;

namespace TodoListApi.Application
{
    public class TaskMapper : Profile
    {
        public TaskMapper()
        {
            CreateMap<Todo, TodoGetDto>();
            CreateMap<TodoCreateDto, Todo>().ReverseMap();
        }
    }
}
