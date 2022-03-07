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
using ToDoApp.Tests.FakeData;

namespace ToDoApp.Tests.Services
{
    public class ToDoItemServiceFake : IToDoItemService
    {
        private IMapper _mapper;
        private List<ToDoItem> _toDoItems;
        private ToDoFakeData _toDoItemsFake;
        public ToDoItemServiceFake(IMapper mapper)
        {
            _mapper = mapper;
            _toDoItemsFake = new ToDoFakeData();
            _toDoItems = _toDoItemsFake.GetToDoItems();
        }

        public async Task<IEnumerable<ToDoItemModel>> GetAllToDoItems()
        {
            return _toDoItems.Select(t => _mapper.Map<ToDoItemModel>(t)).ToList();
        }

        public async Task<ToDoItemModel> GetById(Guid id)
        {
            return _toDoItems.Where(t => t.Id == id).Select(t => _mapper.Map<ToDoItemModel>(t)).FirstOrDefault();
        }

        public async Task<IEnumerable<ToDoItemModel>> GetByName(string name)
        {
            return _toDoItems.Where(t => t.Name.ToLower().Contains(name.ToLower())).Select(t => _mapper.Map<ToDoItemModel>(t)).ToList();
        }

        public async Task<ToDoItemModel> GetByNameEqual(string name)
        {
            var todo = _toDoItems.Where(t => t.Name == name).FirstOrDefault();
            return todo == null ? null : _mapper.Map<ToDoItemModel>(todo);
        }

        public async Task<IEnumerable<ToDoItemModel>> GetByCategory(Guid categoryId)
        {
            return _toDoItems.Where(t => t.CategoryId == categoryId).Select(t => _mapper.Map<ToDoItemModel>(t)).ToList();
        }

        public async Task<IEnumerable<ToDoItemModel>> GetByPriority(ToDoPriority priority)
        {
            return _toDoItems.Where(t => t.Priority == priority).Select(t => _mapper.Map<ToDoItemModel>(t)).ToList();
        }

        public async Task Create(ToDoItem toDoItem)
        {
            _toDoItems.Add(toDoItem);
        }

        public async Task Remove(Guid id)
        {
            var todo = _toDoItems.Where(t => t.Id == id).FirstOrDefault();
            _toDoItems.Remove(todo);
        }

        public async Task Update(ToDoItem toDoItem)
        {
            var todo = _toDoItems.Where(t => t.Id == toDoItem.Id).FirstOrDefault();
            _toDoItems.Remove(todo);
            _toDoItems.Add(toDoItem);
        }
    }
}
