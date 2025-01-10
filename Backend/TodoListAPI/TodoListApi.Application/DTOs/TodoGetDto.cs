using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListApi.Application.DTOs
{
    public record TodoGetDto(
        int TodoId,
        string Title
    );
}
