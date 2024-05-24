using Microsoft.EntityFrameworkCore;
using TodoApiMinimalNet8;

var builder = WebApplication.CreateBuilder(args); // Initialize default web app

// ADD DI
builder.Services.AddDbContext<TodoDbContext>(opt => opt.UseInMemoryDatabase("TodoList"));

var app = builder.Build();

// ADD Middlewares

app.MapGet("/todoitems", async (TodoDbContext context) =>
{
    return await context.TodoItems.ToListAsync();
});

app.MapGet("/todoitems/{id}", async (int id, TodoDbContext context) =>
{
    return await context.TodoItems.FindAsync(id);
});

app.MapPost("/todoitems", async (TodoItem todo, TodoDbContext context) =>
{
    context.TodoItems.Add(todo);
    await context.SaveChangesAsync();
    return Results.Created($"/todoitems/{todo.Id}", todo);
});

app.MapPut("/todoitems/{id}", async (int id, TodoItem inputTodo, TodoDbContext context) =>
{
    var todo = await context.TodoItems.FindAsync(id);
    if (todo == null)
        return Results.NotFound();
    todo.Name = inputTodo.Name;
    todo.IsCompleted = inputTodo.IsCompleted;
    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (int id, TodoDbContext context) =>
{
    var todo = await context.TodoItems.FindAsync(id);
    context.Remove(todo);
    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.Run(); // Start listen web requests
