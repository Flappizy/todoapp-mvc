using System;
using System.Collections.Generic;
using System.Text;

namespace TodoMVC.Data.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
