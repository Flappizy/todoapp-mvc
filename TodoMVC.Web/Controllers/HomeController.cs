using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoMVC.Data.Models;
using TodoMVC.Services;
using TodoMVC.Web.Models;

namespace TodoMVC.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITodoListServices _services; 

        public HomeController(ITodoListServices services)
        {
            _services = services;
        }

        public async Task<IActionResult> Index(bool? completed)
        {
            var todoItems = await
                _services.GetAllAsync(0, 10);

            var items = ToViewModels(todoItems);

            if(completed != null)
            {
                items = items.Where(x => x.IsCompleted == completed);
            }

            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TodoItemCreateModel model = new TodoItemCreateModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoItemCreateModel todoItem)
        {
            var item = ToTodoItem(todoItem);

            _services.AddAsync(item).Wait();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var todoItem = await _services.GetByIdAsync(id);

            if (todoItem != null)
            {
                var model = ToEditModel(todoItem);

                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TodoItemEditModel model)
        {
            var todoItem = await _services.GetByIdAsync(model.Id);

            todoItem.Title = model.Title;
            todoItem.IsCompleted = model.IsCompleted;

            _services.EditAsync(todoItem).Wait();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _services.RemoveAsync(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ClearCompleted()
        {
            _services.ClearCompletedAsync().Wait();

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private TodoItem ToTodoItem(TodoItemCreateModel model)
        {
            var todoItem = new TodoItem
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                IsCompleted = model.IsCompleted
            };

            return todoItem;
        }

        private IEnumerable<TodoItemViewModel> ToViewModels(IEnumerable<TodoItem> collention)
        {
            IEnumerable<TodoItemViewModel> items = collention.Select(x =>
                new TodoItemViewModel
                {
                    Id = x.Id,
                    IsCompleted = x.IsCompleted,
                    Title = x.Title
                }
            );

            return items;
        }

        private TodoItemEditModel ToEditModel(TodoItem item)
        {
            var model = new TodoItemEditModel
            {
                Id = item.Id,
                Title = item.Title,
                IsCompleted = item.IsCompleted
            };

            return model;
        }
    }
}
