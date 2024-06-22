using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TodoApi.Models;
using System.Linq;

namespace ToDoApi.Controllers
{
    [Route("api/Todo")]
    public class TodoController : Controller
    {
        private readonly TodoContext _todoContext;
        public TodoController(TodoContext todoContext)
        {
            _todoContext = todoContext;
            if (_todoContext.TodoItems.Count() == 0)
            {
                _todoContext.TodoItems.Add(new Models.TodoItem { Name = "Item1" });
                _todoContext.SaveChanges();
            }
        }




        [HttpGet]
        public IEnumerable<Models.TodoItem> GetAll()
        {
            return _todoContext.TodoItems.ToList();
        }
        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById (long id)
        {
            var item=_todoContext.TodoItems.FirstOrDefault(x => x.Id == id);
            if(item == null)
                return NotFound();
            return new ObjectResult(item);
        }



        [HttpPost]
        public IActionResult Create([FromBody]  Models.TodoItem item)
        {
            if(item == null)
                return BadRequest();
            _todoContext.TodoItems.Add(item);
            _todoContext.SaveChanges();
            return CreatedAtRoute("GetTodo", new {id=item.Id },item);
        }
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Models.TodoItem item)
        {
            if(item==null||item.Id!=id) return BadRequest();
            var todo= _todoContext.TodoItems.FirstOrDefault( i=> i.Id==id);
            if (todo==null) return NotFound();
            todo.IsComplete=item.IsComplete;
            todo.Name=item.Name;

            _todoContext.TodoItems.Update(todo);
            _todoContext.SaveChanges();
            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var todo=_todoContext.TodoItems.FirstOrDefault(i=> i.Id==id);
            if (todo==null) return NotFound();
            _todoContext.TodoItems.Remove(todo);
            _todoContext.SaveChanges();
            return new NoContentResult();
        }
    }
}
