using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListApi.Application.DTOs;

namespace TodoListApi.Application
{
    public class TodoCreateDtoValidator : AbstractValidator<TodoCreateDto>
    {
        public TodoCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MinimumLength(2).WithMessage("Min symbol is 2");

        }
    }
}
