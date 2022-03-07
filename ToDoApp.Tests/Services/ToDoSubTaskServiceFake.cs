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
    public class ToDoSubTaskServiceFake : IToDoSubTaskService
    {
        private IMapper _mapper;
        private List<SubTask> _toDoSubTasks;
        private ToDoFakeData _toDoSubTasksFake;
        public ToDoSubTaskServiceFake(IMapper mapper)
        {
            _mapper = mapper;
            _toDoSubTasksFake = new ToDoFakeData();
            _toDoSubTasks = _toDoSubTasksFake.GetSubTasks();
        }

        public async Task<List<SubTaskModel>> GetAllForToDo(Guid toDoTaskId)
        {
            return _toDoSubTasks.Where(s => s.ToDoItemId == toDoTaskId).Select(s => _mapper.Map<SubTaskModel>(s)).ToList();
        }

        public async Task<SubTaskModel> GetById(Guid id)
        {
            var subTask = _toDoSubTasks.Where(s => s.Id == id).FirstOrDefault();
            return subTask == null ? null : _mapper.Map<SubTaskModel>(subTask);
        }

        public async Task SetDone(Guid id)
        {
            var subTask = _toDoSubTasks.Where(s => s.Id == id).FirstOrDefault();
            _toDoSubTasks.Remove(subTask);
            subTask.IsDone = true;
            _toDoSubTasks.Add(subTask);
        }

        public async Task Create(SubTask model)
        {
            _toDoSubTasks.Add(model);
        }
    }
}
