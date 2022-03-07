using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Business.Models;
using ToDoApp.Business.Services.Base;
using ToDoApp.Domain.Entity;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Business.Services
{
    public class ToDoCategoryService : IToDoCategoryService
    {
        private readonly IMapper _mapper;

        private IToDoCategoryRepository _toDoCategoryRepository;
        public ToDoCategoryService(IMapper mapper,
            IToDoCategoryRepository toDoCategoryRepository)
        {
            _mapper = mapper;
            _toDoCategoryRepository = toDoCategoryRepository;
        }

        public async Task<IEnumerable<ToDoCategoryModel>> GetAllCategories()
        {
            var categories = await _toDoCategoryRepository.GetAll();
            return categories.Select(c => _mapper.Map<ToDoCategoryModel>(c)).ToList();
        }

        public async Task<ToDoCategoryModel> GetById(Guid id)
        {
            var category = await _toDoCategoryRepository.GetById(id);
            return category == null ? null : _mapper.Map<ToDoCategoryModel>(category);
        }

        public async Task<IEnumerable<ToDoCategoryModel>> GetByName(string name)
        {
            var categories = await _toDoCategoryRepository.Find(c => c.Name.ToLower().Contains(name));
            return categories.Select(c => _mapper.Map<ToDoCategoryModel>(c)).ToList();
        }

        public async Task<IEnumerable<ToDoCategoryModel>> GetByShortName(string shortName)
        {
            var categories = await _toDoCategoryRepository.Find(c => c.ShortName.ToLower().Contains(shortName));
            return categories.Select(c => _mapper.Map<ToDoCategoryModel>(c)).ToList();
        }

        public async Task Create(ToDoCategory category)
        {            
            await _toDoCategoryRepository.Add(category);
            await _toDoCategoryRepository.SaveChangesAsync();
        }

        public async Task Update(ToDoCategory category)
        {
            _toDoCategoryRepository.Update(category);
            await _toDoCategoryRepository.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
        {
            var category = await _toDoCategoryRepository.GetById(id);
            await _toDoCategoryRepository.Remove(category);
        }
    }
}
