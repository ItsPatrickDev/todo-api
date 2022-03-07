using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Business.Models;
using ToDoApp.Domain.Entity;
using ToDoApp.Domain.Enums;

namespace ToDoApp.Business.Services.Base
{
    public interface IToDoItemService
    {
        Task<IEnumerable<ToDoItemModel>> GetAllToDoItems();
        Task<ToDoItemModel> GetById(Guid id);
        Task<IEnumerable<ToDoItemModel>> GetByName(string name);
        Task<ToDoItemModel> GetByNameEqual(string name);
        Task<IEnumerable<ToDoItemModel>> GetByCategory(Guid categoryId);
        Task<IEnumerable<ToDoItemModel>> GetByPriority(ToDoPriority priority);
        Task Create(ToDoItem toDoItem);
        Task Remove(Guid id);
        Task Update(ToDoItem toDoItem);
    }
}
