using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Business.Models;
using ToDoApp.Domain.Entity;

namespace ToDoApp.Business.Services.Base
{
    public interface IToDoSubTaskService
    {
        Task<SubTaskModel> GetById(Guid id);
        Task<List<SubTaskModel>> GetAllForToDo(Guid toDoTaskId);
        Task SetDone(Guid id);
        Task Create(SubTask model);
    }
}
