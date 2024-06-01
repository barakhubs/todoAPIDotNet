using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoAPI.AppDataContext;
using TodoAPI.Contracts;
using TodoAPI.Interfaces;
using TodoAPI.Models;

namespace TodoAPI.Services
{

    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _context;
        private readonly ILogger<TodoService> _logger;
        private readonly IMapper _mapper;

        public TodoService(TodoDbContext context, ILogger<TodoService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task CreateTodoAsync(CreateTodoRequest request)
        {
            try
            {
                var todo = _mapper.Map<Todo>(request);
                todo.CreatedAt = DateTime.UtcNow;
                _context.Todos.Add(todo);
                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An error occured while creating the Todo item.");
                throw new Exception("An error occured while creating the Todo item.");
            }
        }

        public async Task DeleteTodoAsync(Guid id)
        {

            var todo = await _context.Todos.FindAsync(id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();

            }
            else
            {
                throw new Exception($"No  item found with the id {id}");
            }


        }

        public async Task<Todo> GetByIdAsync(Guid Id)
        {
            var todo = await _context.Todos.FindAsync(Id);
            if (todo == null)
            {
                throw new KeyNotFoundException($"No todo item with Id: {Id} found");
            }

            return todo;
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            var todo = await _context.Todos.ToListAsync();
            if (todo == null)
            {
                throw new Exception(" No Todo items found");
            }
            return todo;

        }

        public async Task UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            try
            {
                var todo = await _context.Todos.FindAsync(id);
                if (todo == null)
                {
                    throw new Exception($"Todo item with id {id} not found.");
                }

                if (request.Title != null)
                {
                    todo.Title = request.Title;
                }

                if (request.Description != null)
                {
                    todo.Description = request.Description;
                }

                if (request.DueDate != null)
                {
                    todo.DueDate = request.DueDate.Value;
                }

                if (request.Priority != null)
                {
                    todo.Priority = request.Priority.Value;
                }

                if (request.IsComplete == true)
                {
                    todo.IsComplete = true;
                }

                todo.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the todo item with id {id}.");
                throw;
            }
        }
    }
}