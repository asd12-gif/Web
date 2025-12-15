using Lab5.Context; 
using Lab5.Models; 
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!"); 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//static TodoDTO ItemToDTO(TodoItems todoItem) => new TodoDTO(todoItem);

//app.MapGet("/api/todoitems", async (TodoDBContext db) =>
//{
//    var todoItems = await db.ToDoItems.ToListAsync();
//    return Results.Ok(todoItems.Select(x => ItemToDTO(x)).ToList());
//})
//.WithName("GetAllToDoItems")
//.WithOpenApi();

//app.MapGet("/api/todoitems/{id}", async (int id, TodoDBContext db) =>
//    await db.ToDoItems.FindAsync(id)
//        is TodoItems todo
//            ? Results.Ok(ItemToDTO(todo))
//            : Results.NotFound())
//    .WithName("GetToDoItemById")
//    .WithOpenApi();

//app.MapPost("/api/todoitems", async (TodoDTO todoDTO, TodoDBContext db) =>
//{

//    var todo = new TodoItems
//    {
//        Name = todoDTO.Name,
//        IsComplete = todoDTO.IsComplete
//    };

//    db.ToDoItems.Add(todo);
//    await db.SaveChangesAsync();
//    return Results.Created($"/api/todoitems/{todo.Id}", ItemToDTO(todo));
//})
//.WithName("CreateToDoItem")
//.WithOpenApi();

//app.MapPut("/api/todoitems/{id}", async (int id, TodoDTO inputTodoDTO, TodoDBContext db) =>
//{
    
//    if (id != inputTodoDTO.Id)
//    {
//        return Results.BadRequest();
//    }

//    var todo = await db.ToDoItems.FindAsync(id);

//    if (todo is null) return Results.NotFound();

//    todo.Name = inputTodoDTO.Name;
//    todo.IsComplete = inputTodoDTO.IsComplete;

//    await db.SaveChangesAsync();
//    return Results.NoContent();
//})
//.WithName("UpdateToDoItem")
//.WithOpenApi();

//app.MapDelete("/api/todoitems/{id}", async (int id, TodoDBContext db) =>
//{
//    if (await db.ToDoItems.FindAsync(id) is TodoItems todo)
//    {
//        db.ToDoItems.Remove(todo);
//        await db.SaveChangesAsync();
//        return Results.Ok(ItemToDTO(todo));
//    }
//    return Results.NotFound();
//})
//.WithName("DeleteToDoItem")
//.WithOpenApi();

app.MapControllers();
app.Run();