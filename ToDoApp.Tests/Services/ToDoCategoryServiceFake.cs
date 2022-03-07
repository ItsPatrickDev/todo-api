using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Business.Models;
using ToDoApp.Business.Services.Base;
using ToDoApp.Domain.Entity;
using ToDoApp.Tests.FakeData;

namespace ToDoApp.Tests.Services
{
    public class ToDoCategoryServiceFake : IToDoCategoryService
    {
        private IMapper _mapper;
        private List<ToDoCategory> _toDoCategories;
        private ToDoFakeData _toDoItemsFake;
        public ToDoCategoryServiceFake(IMapper mapper)
        {
            _mapper = mapper;
            _toDoItemsFake = new ToDoFakeData();
            _toDoCategories = _toDoItemsFake.GetToDoCategories();
        }

        public async Task<IEnumerable<ToDoCategoryModel>> GetAllCategories()
        {
            return _toDoCategories.Select(t => _mapper.Map<ToDoCategoryModel>(t)).ToList();
        }

        public async Task<ToDoCategoryModel> GetById(Guid id)
        {
            var category = _toDoCategories.Where(t => t.Id == id).FirstOrDefault();
            return category == null ? null : _mapper.Map<ToDoCategoryModel>(category);
        }

        public async Task<IEnumerable<ToDoCategoryModel>> GetByName(string name)
        {
            var categories = _toDoCategories.Where(t => t.Name.ToLower().Contains(name.ToLower())).ToList();
            return categories.Select(c => _mapper.Map<ToDoCategoryModel>(c)).ToList();
        }

        public async Task<IEnumerable<ToDoCategoryModel>> GetByShortName(string shortName)
        {
            var categories = _toDoCategories.Where(t => t.ShortName.ToLower().Contains(shortName.ToLower())).ToList();
            return categories.Select(c => _mapper.Map<ToDoCategoryModel>(c)).ToList();
        }

        public async Task Create(ToDoCategory category)
        {
            _toDoCategories.Add(category);
        }

        public async Task Remove(Guid id)
        {
            var category = _toDoCategories.Where(t => t.Id == id).FirstOrDefault();
            _toDoCategories.Remove(category);
        }

        public async Task Update(ToDoCategory category)
        {
            var categoryItem = _toDoCategories.Where(t => t.Id == category.Id).FirstOrDefault();
            _toDoCategories.Remove(categoryItem);
            _toDoCategories.Add(category);
        }
    }
}
