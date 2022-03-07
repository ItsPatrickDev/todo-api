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
    public class ToDoItemRepository : BaseRepository<ToDoItem>, IToDoItemRepository
    {
        private ApplicationDbContext _dbContext;
        public ToDoItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ToDoItem>> GetAllIncludes()
        {
            return await _dbContext.ToDoItems
                .Include(t => t.Category)
                .Include(t => t.SubTasks)
                .ToListAsync();
        }

        public async Task<IEnumerable<ToDoItem>> GetAllIncludes(Expression<Func<ToDoItem, bool>> expression)
        {
            return await _dbContext.ToDoItems
                .Include(t => t.Category)
                .Include(t => t.SubTasks)
                .Where(expression)
                .ToListAsync();
        }
    }
}
