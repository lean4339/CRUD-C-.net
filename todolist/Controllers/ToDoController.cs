using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todolist.infraestructura;
using todolist.Models;

namespace todolist.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoContext context;
        public ToDoController(ToDoContext context)
        {
            this.context = context;
        }
        public async Task<ActionResult> Index()
        {
            IQueryable<TodoList> items = from i in context.TodoList orderby i.Id select i;

            List<TodoList> todolist = await items.ToListAsync();

            return View(todolist);
        }

        // get todo create
        public IActionResult Create() => View();

        // post todo create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoList item)
        {
            if (ModelState.IsValid)
            {
                context.Add(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "item agregado";

                return RedirectToAction("Index");
            }
            return View(item);
        }

        // get todo edit / id

        public async Task<ActionResult> Edit(int id)
        {
            TodoList item = await context.TodoList.FindAsync(id);
            if(item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // post todo edit / id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TodoList item)
        {
            if (ModelState.IsValid)
            {
                context.Update(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "Item updated";

                return RedirectToAction("Index");
            }
            return View(item);
        }

        // get todo delet / id
        public async Task<ActionResult> Delete(int id)
        {
            TodoList item = await context.TodoList.FindAsync(id);
            if( item == null)
            {
                return NotFound();
            }
            context.TodoList.Remove(item);
            await context.SaveChangesAsync();
            TempData["Succes"] = "The item has been deleted";
            return RedirectToAction("Index");
        }
    }
}
