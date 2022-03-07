using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Entity;

namespace ToDoApp.Domain.Repositories
{
    public interface ISubTaskRepository : IBaseRepository<SubTask>
    {
        Task<IEnumerable<ToDoItem>> GetIncludesSubTasks();
        Task<IEnumerable<ToDoItem>> GetIncludesSubTasks(Expression<Func<ToDoItem, bool>> expression);
    }
}
