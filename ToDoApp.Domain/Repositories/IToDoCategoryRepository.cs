using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Entity;

namespace ToDoApp.Domain.Repositories
{
    public interface IToDoCategoryRepository : IBaseRepository<ToDoCategory>
    {
        Task<IEnumerable<ToDoItem>> GetIncludesCategory();
        Task<IEnumerable<ToDoItem>> GetIncludesCategory(Expression<Func<ToDoItem, bool>> expression);
    }
}
