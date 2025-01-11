using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListApi.Domain.Entities
{
    public class Todo
    {
        public int TodoId { get; set; }
        public string Title { get; set; }
    }
}
