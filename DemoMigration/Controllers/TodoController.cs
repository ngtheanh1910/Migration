using DemoMigration.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMigration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly MyDbContext _context;
        public TodoController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var lstTodo = _context.Todos.ToList();
            return Ok(lstTodo);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var todo = _context.Todos.SingleOrDefault(t => t.Id == id);
            if (todo != null)
            {
                return Ok(todo);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Create(Todo addTodo)
        {
            Todo todo = new Todo();
            todo.Title = addTodo.Title;
            todo.AddDate = DateTime.Now;
            todo.IsDone = false;
            _context.Todos.Add(todo);
            _context.SaveChanges();
            return Ok(new { Success = true, Data = todo});
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, Todo editTodo)
        {
            var todo = _context.Todos.FirstOrDefault(t => t.Id == id);
            if (todo == null) 
            {
                return NotFound();
            }
            todo.Title = editTodo.Title;
            todo.AddDate = editTodo.AddDate;
            todo.IsDone = editTodo.IsDone;
            _context.SaveChanges();
            return Ok(new { Success = true, Data = todo });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _context.Todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            _context.Todos.Remove(todo);
            _context.SaveChanges();
            return Ok(new { Success = true, Data = todo });
        }
    }
}
