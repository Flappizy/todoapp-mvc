using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoMVC.Web.Models
{
    public class TodoListViewModel
    {
        public IEnumerable<TodoItemViewModel> TodoItems { get; set; }

        public TodoItemViewModel TodoItem { get; set; }
    }
}
