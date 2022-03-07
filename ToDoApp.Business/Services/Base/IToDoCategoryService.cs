using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Business.Models;
using ToDoApp.Domain.Entity;

namespace ToDoApp.Business.Services.Base
{
    public interface IToDoCategoryService
    {
        Task<IEnumerable<ToDoCategoryModel>> GetAllCategories();
        Task<ToDoCategoryModel> GetById(Guid id);
        Task<IEnumerable<ToDoCategoryModel>> GetByName(string name);
        Task<IEnumerable<ToDoCategoryModel>> GetByShortName(string shortName);
        Task Create(ToDoCategory category);
        Task Update(ToDoCategory category);
        Task Remove(Guid id);
    }
}
