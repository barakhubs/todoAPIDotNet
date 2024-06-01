using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Contracts;
using TodoAPI.Models;

namespace TodoAPI.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo> GetByIdAsync(Guid Id);
        Task CreateTodoAsync(CreateTodoRequest request);
        Task UpdateTodoAsync(Guid Id, UpdateTodoRequest request);
        Task DeleteTodoAsync(Guid Id);
    }
}