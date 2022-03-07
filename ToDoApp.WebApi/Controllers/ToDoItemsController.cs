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
using ToDoApp.Business.Services.Base;
using ToDoApp.Domain.Entity;
using ToDoApp.Domain.Enums;

namespace ToDoApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/todo")]
    public class ToDoItemsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ToDoItemsController> _logger;

        private IToDoItemService _toDoItemService;
        public ToDoItemsController(ILogger<ToDoItemsController> logger,
            IMapper mapper,
            IToDoItemService toDoItemService)
        {
            _logger = logger;
            _mapper = mapper;
            _toDoItemService = toDoItemService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get all TODO items")]
        public async Task<IActionResult> GetAll()
        {
            var toDoItems = await _toDoItemService.GetAllToDoItems();
            return Ok(toDoItems);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Get item by id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var toDoItem = await _toDoItemService.GetById(id);
            if (toDoItem == null)
                return NotFound();

            return Ok(toDoItem);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get TODOs by name or letters")]
        public async Task<IActionResult> GetByName(string name)
        {
            var toDoItems = await _toDoItemService.GetByName(name);
            return Ok(toDoItems);
        }

        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get TODOs by categoryId")]
        public async Task<IActionResult> GetByCategory(Guid categoryId)
        {
            var toDoItems = await _toDoItemService.GetByCategory(categoryId);
            return Ok(toDoItems);
        }

        [HttpGet("priority/{priority}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get TODOs by priority")]
        public async Task<IActionResult> GetByPriority(ToDoPriority priority)
        {
            var toDoItems = await _toDoItemService.GetByPriority(priority);
            return Ok(toDoItems);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [SwaggerOperation(Summary = "Create new TODO")]
        public async Task<IActionResult> Create(ToDoItem model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var itemByName = await _toDoItemService.GetByNameEqual(model.Name);
                if (itemByName != null)
                    return BadRequest("TODO with that name already exists");

                model.Id = Guid.NewGuid();
                model.CreationDate = DateTime.Now;

                await _toDoItemService.Create(model);
                return Ok();
            }
            catch(Exception e)
            {
                _logger.LogError("Bad Request - {0}", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Update TODO")]
        public async Task<IActionResult> Update(ToDoItem model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _toDoItemService.Update(model);
                return Ok();
            }
            catch(Exception e)
            {
                _logger.LogError("Bad Request - {0}", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Delete todo (DONE)")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var item = await _toDoItemService.GetById(id);
                if (item == null)
                    return BadRequest("No TODO with a given id");

                await _toDoItemService.Remove(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Bad Request - {0}", e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
