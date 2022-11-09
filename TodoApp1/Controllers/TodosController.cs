using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp1.Data;
using TodoApp1.Models;

namespace TodoApp1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : Controller
    {
        private readonly TodosAPIDbContext dbContext;

        public TodosController(TodosAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetTodos()

        {
            if (dbContext.Todos == null)
                return NotFound();

            return Ok(await dbContext.Todos.ToListAsync());

        }

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetTodo([FromRoute] Guid id)
        {
            var todo = await dbContext.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);

        }



        [HttpPost]
        public async Task<IActionResult> AddTodo(AddTodoRequest addTodoRequest)
        {
            var todo = new Todo()
            {
                Id = Guid.NewGuid(),
                Email = addTodoRequest.Email,
                Title = addTodoRequest.Title,
                Status = addTodoRequest.Status,
                Date = addTodoRequest.Date,

            };

            await dbContext.Todos.AddAsync(todo);
            await dbContext.SaveChangesAsync();


            return Ok(todo);



        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateTodo([FromRoute] Guid id, UpdateTodoRequest updateTodoRequest)
        {
            var todo = await dbContext.Todos.FindAsync(id);

            if (todo != null)
            {
                todo.Email = updateTodoRequest.Email;
                todo.Title = updateTodoRequest.Title;
                todo.Status = updateTodoRequest.Status;
                todo.Date = updateTodoRequest.Date;

                await dbContext.SaveChangesAsync();

                return Ok(todo);

            }

            return NotFound();

        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteTodo([FromRoute] Guid id)
        {
            var todo = await dbContext.Todos.FindAsync(id);

            if (todo != null)
            {
                dbContext.Todos.Remove(todo);
                await dbContext.SaveChangesAsync();
                return Ok(todo);
            }

            return NotFound();


        }

        [HttpGet("{page}")]
        public async Task<ActionResult<List<Todo>>>GetTodos(int page)
        {
            if(dbContext.Todos == null)
                return NotFound();

            var pageResults = 3f;
            var pageCount = Math.Ceiling(dbContext.Todos.Count() / pageResults);

            var todoItems = await dbContext.Todos
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            var Response = new TodoResponse
            {
                Todos = todoItems,
                CurrentPage = page,
                Pages = (int)pageCount

            };

            return Ok(Response);

        }


    }


}
