using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoMVC.Data;
using TodoMVC.Data.Models;

namespace TodoMVC.Services
{
    public class TodoListServices : ITodoListServices
    {
        private readonly TodoListDbContext _context;

        public TodoListServices(TodoListDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TodoItem todoItem)
        {
            await _context.TodoItems.AddAsync(todoItem);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(TodoItem todoItem)
        {
            TodoItem item = _context.TodoItems.SingleOrDefault(x => x.Id == todoItem.Id);

            if(item != null)
            {
                item.IsCompleted = todoItem.IsCompleted;
                item.Title = todoItem.Title;
                _context.TodoItems.Update(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync(int page, int pageCount)
        {
            IEnumerable<TodoItem> result = _context.TodoItems.ToList();

            result = result.Skip(page * pageCount).Take(pageCount);

            return result;
        }

        public async Task<TodoItem> GetByIdAsync(Guid id)
        {
            TodoItem item = _context.TodoItems.SingleOrDefault(x => x.Id == id);

            return item;
        }

        public async Task RemoveAsync(Guid id)
        {
            TodoItem item = _context.TodoItems.SingleOrDefault(x => x.Id == id);

            if(item != null)
            {
                _context.TodoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
