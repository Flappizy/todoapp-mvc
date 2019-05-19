using System;
using TodoMVC.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoMVC.Services
{
    public interface ITodoListServices
    {
        Task AddAsync(TodoItem todoItem);

        Task<IEnumerable<TodoItem>> GetAllAsync(int page, int pageCount);

        Task<TodoItem> GetByIdAsync(Guid id);

        Task RemoveAsync(Guid id);

        Task EditAsync(TodoItem todoItem);

        Task ClearCompletedAsync();
    }
}
