using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoMVC.Web.Models
{
    public class TodoItemCreateModel
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public bool IsCompleted { get; set; }
    }
}
