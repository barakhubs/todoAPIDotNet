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
                var todo = _mapper.Map<Todo> (request);
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

        public Task DeleteTodoAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Todo> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            var todo= await _context.Todos.ToListAsync();
            if (todo == null)
            {
                throw new Exception(" No Todo items found");
            }
            return todo;

        }

        public Task UpdateTodoAsync(Guid Id, UpdateTodoRequest request)
        {
            throw new NotImplementedException();
        }
    }
}