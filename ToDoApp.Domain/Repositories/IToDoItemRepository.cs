using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Entity;

namespace ToDoApp.Domain.Repositories
{
    public interface IToDoItemRepository : IBaseRepository<ToDoItem>
    {
        Task<IEnumerable<ToDoItem>> GetAllIncludes();
        Task<IEnumerable<ToDoItem>> GetAllIncludes(Expression<Func<ToDoItem, bool>> expression);
    }
}
