using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using asp.net_tutorial.Models;

/*this defines API Controller without methods, decorates the class with the API controller attribute (indicates that the
 controller responds to web API requests, and uses DI to inject the db context into the controller. The db context is used in
 each of the CRUD methods in the controller

Operation 	        SQL 	HTTP 	            RESTful WS 	DDS
Create 	            INSERT 	PUT / POST 	        POST 	    write
Read (Retrieve) 	SELECT 	GET 	            GET 	    read / take
Update (Modify) 	UPDATE 	PUT / POST / PATCH 	PUT 	    write
Delete (Destroy) 	DELETE 	DELETE 	            DELETE 	    dispose 

 */

namespace asp.net_tutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);

            /*The preceding code is an HTTP POST method, as indicated by the [HttpPost] attribute. 
            The method gets the value of the to-do item from the body of the HTTP request. 
            The CreatedAtAction method:
                -Returns an HTTP 201 status code if successful. HTTP 201 is the standard response for an HTTP POST 
                method that creates a new resource on the server.
                -Adds a Location header to the response. The Location header specifies the URI of the newly created to-do item. 
                -References the GetTodoItem action to create the Location header's URI. The C# nameof keyword is used 
                to avoid hard-coding the action name in the CreatedAtAction call.
            */
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
