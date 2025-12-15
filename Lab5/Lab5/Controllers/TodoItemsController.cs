using Lab5.Context;
using Lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoDBContext _context;

        // Constructor: Dependency Injection
        public TodoItemsController(TodoDBContext context)
        {
            _context = context;
        }

        // Hàm chuyển đổi từ Entity sang DTO
        private static TodoDTO ItemToDTO(TodoItems todoItem) => new TodoDTO(todoItem);


        // 1. GET ALL: GET /api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoDTO>>> GetTodoItems()
        {
            // Sử dụng Select để tránh lỗi EF Core Translation
            return await _context.ToDoItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // 2. GET BY ID: GET /api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDTO>> GetTodoItem(int id)
        {
            var todoItem = await _context.ToDoItems.FindAsync(id);
            if (todoItem == null) return NotFound();
            return ItemToDTO(todoItem);
        }

        // 3. POST (CREATE): POST /api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoDTO>> PostTodoItem(TodoDTO todoDTO)
        {
            var todoItem = new TodoItems
            {
                Name = todoDTO.Name,
                IsComplete = todoDTO.IsComplete
            };
            _context.ToDoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, ItemToDTO(todoItem));
        }

        // 4. PUT (UPDATE): PUT /api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoDTO todoDTO)
        {
            if (id != todoDTO.Id) return BadRequest();
            var todoItem = await _context.ToDoItems.FindAsync(id);
            if (todoItem == null) return NotFound();

            todoItem.Name = todoDTO.Name;
            todoItem.IsComplete = todoDTO.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!_context.ToDoItems.Any(e => e.Id == id))
            {
                return NotFound();
            }
            return NoContent();
        }

        // 5. DELETE: DELETE /api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var todoItem = await _context.ToDoItems.FindAsync(id);
            if (todoItem == null) return NotFound();

            _context.ToDoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
