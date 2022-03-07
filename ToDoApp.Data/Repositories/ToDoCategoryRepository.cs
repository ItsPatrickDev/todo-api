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
    public class ToDoCategoryRepository : BaseRepository<ToDoCategory>, IToDoCategoryRepository
    {
        private ApplicationDbContext _dbContext;
        public ToDoCategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ToDoItem>> GetIncludesCategory()
        {
            return await _dbContext.ToDoItems
                .Include(t => t.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<ToDoItem>> GetIncludesCategory(Expression<Func<ToDoItem, bool>> expression)
        {
            return await _dbContext.ToDoItems
                .Include(t => t.Category)
                .Where(expression)
                .ToListAsync();
        }
    }
}
