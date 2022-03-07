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
    public class ToDoSubTaskService : IToDoSubTaskService
    {
        private readonly IMapper _mapper;

        private ISubTaskRepository _subTaskRepository;
        private IToDoItemRepository _toDoItemRepository;
        public ToDoSubTaskService(IMapper mapper,
            ISubTaskRepository subTaskRepository,
            IToDoItemRepository toDoItemRepository)
        {
            _mapper = mapper;
            _subTaskRepository = subTaskRepository;
            _toDoItemRepository = toDoItemRepository;
        }

        public async Task<SubTaskModel> GetById(Guid id)
        {
            var subTask = await _subTaskRepository.GetById(id);
            return subTask == null ? null : _mapper.Map<SubTaskModel>(subTask);
        }

        public async Task<List<SubTaskModel>> GetAllForToDo(Guid toDoTaskId)
        {
            var todo = await _subTaskRepository.GetIncludesSubTasks(t => t.Id == toDoTaskId);
            if (!todo.Any())
                return new List<SubTaskModel>();

            var todoModel = todo.FirstOrDefault();
            List<SubTask> subTask = null;
            if (todoModel != null)
            {
                subTask = todoModel.SubTasks.ToList();
                return subTask.Select(s => _mapper.Map<SubTaskModel>(s)).ToList();
            }
            return new List<SubTaskModel>();
        }

        public async Task SetDone(Guid id)
        {
            var subTask = await _subTaskRepository.GetById(id);
            subTask.IsDone = true;
            await _subTaskRepository.SaveChangesAsync();
        }

        public async Task Create(SubTask model)
        {
            await _subTaskRepository.Add(model);
            await _subTaskRepository.SaveChangesAsync();
        }
    }
}
