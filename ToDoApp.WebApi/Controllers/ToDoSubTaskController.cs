using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Business.Models;
using ToDoApp.Business.Models.SubTask;
using ToDoApp.Business.Services.Base;
using ToDoApp.Domain.Entity;

namespace ToDoApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/subtask")]
    public class ToDoSubTaskController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ToDoSubTaskController> _logger;

        private IToDoSubTaskService _toDoSubTaskService;
        public ToDoSubTaskController(ILogger<ToDoSubTaskController> logger,
            IMapper mapper,
            IToDoSubTaskService toDoSubTaskService)
        {
            _logger = logger;
            _mapper = mapper;
            _toDoSubTaskService = toDoSubTaskService;
        }

        [HttpGet("{toDoTaskId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get subTasks for ToDoTaskId")]
        public async Task<IActionResult> GetAllSubtasks(Guid toDoTaskId)
        {
            var subTasks = await _toDoSubTaskService.GetAllForToDo(toDoTaskId);
            return Ok(subTasks);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Create subTask for TODO")]
        public async Task<IActionResult> Create(CreateSubTaskModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var subTask = _mapper.Map<SubTask>(model);
                await _toDoSubTaskService.Create(subTask);
                return Ok();
            }
            catch(Exception e)
            {
                _logger.LogError("Bad Request - {0}", e.Message);
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Set subTask DONE")]
        public async Task<IActionResult> SetDone(Guid id)
        {
            try
            {
                var item = await _toDoSubTaskService.GetById(id);
                if (item == null)
                    return BadRequest("No SubTask with a given id");

                await _toDoSubTaskService.SetDone(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Bad Request - {0}", e.Message);
                return BadRequest();
            }
        }
    }
}
