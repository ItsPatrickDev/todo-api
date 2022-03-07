using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Entity;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Data.Repositories
{
    public class SubTaskRepository : BaseRepository<SubTask>, ISubTaskRepository
    {
        private ApplicationDbContext _dbContext;
        public SubTaskRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ToDoItem>> GetIncludesSubTasks()
        {
            return await _dbContext.ToDoItems
                .Include(t => t.SubTasks)
                .ToListAsync();
        }

        public async Task<IEnumerable<ToDoItem>> GetIncludesSubTasks(Expression<Func<ToDoItem, bool>> expression)
        {
            return await _dbContext.ToDoItems
                .Include(t => t.SubTasks)
                .Where(expression)
                .ToListAsync();
        }
    }
}
