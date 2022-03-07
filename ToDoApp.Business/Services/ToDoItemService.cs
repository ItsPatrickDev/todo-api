using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Business.Models;
using ToDoApp.Business.Services.Base;
using ToDoApp.Domain.Entity;
using ToDoApp.Domain.Enums;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Business.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly IMapper _mapper;

        private IToDoItemRepository _toDoItemRepository;
        private ISubTaskRepository _subTaskRepository;
        public ToDoItemService(IMapper mapper,
            IToDoItemRepository toDoItemRepository,
            ISubTaskRepository subTaskRepository)
        {
            _mapper = mapper;
            _toDoItemRepository = toDoItemRepository;
            _subTaskRepository = subTaskRepository;
        }
        public async Task<IEnumerable<ToDoItemModel>> GetAllToDoItems()
        {
            var toDoItems = await _toDoItemRepository.GetAllIncludes();
            return toDoItems.Select(t => _mapper.Map<ToDoItemModel>(t)).ToList();
        }

        public async Task<ToDoItemModel> GetById(Guid id)
        {
            var toDoItem = await _toDoItemRepository.GetAllIncludes(t => t.Id == id);
            return toDoItem == null ? null : _mapper.Map<ToDoItemModel>(toDoItem.FirstOrDefault());
        }

        public async Task<IEnumerable<ToDoItemModel>> GetByName(string name)
        {
            var toDoItems = await _toDoItemRepository.GetAllIncludes(t => t.Name.ToLower().Contains(name.ToLower()));
            return toDoItems.Select(t => _mapper.Map<ToDoItemModel>(t)).ToList();
        }

        public async Task<ToDoItemModel> GetByNameEqual(string name)
        {
            var toDoItem = await _toDoItemRepository.GetAllIncludes(t => t.Name == name);
            var todo = toDoItem.FirstOrDefault();
            return todo == null ? null : _mapper.Map<ToDoItemModel>(todo);
        }

        public async Task<IEnumerable<ToDoItemModel>> GetByCategory(Guid categoryId)
        {
            var toDoItems = await _toDoItemRepository.GetAllIncludes(t => t.Category.Id == categoryId);
            return toDoItems.Select(t => _mapper.Map<ToDoItemModel>(t)).ToList();
        }

        public async Task<IEnumerable<ToDoItemModel>> GetByPriority(ToDoPriority priority)
        {
            var toDoItems = await _toDoItemRepository.GetAllIncludes(t => t.Priority == priority);
            return toDoItems.Select(t => _mapper.Map<ToDoItemModel>(t)).ToList();
        }

        public async Task Create(ToDoItem toDoItem)
        {
            await _toDoItemRepository.Add(toDoItem);
            await _toDoItemRepository.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
        {
            var toDoItem = await _toDoItemRepository.GetById(id);
            await _toDoItemRepository.Remove(toDoItem);
        }

        public async Task Update(ToDoItem toDoItem)
        {
            _toDoItemRepository.Update(toDoItem);
            await _toDoItemRepository.SaveChangesAsync();
        }
    }
}
