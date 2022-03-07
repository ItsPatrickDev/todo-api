using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Business.Models;
using ToDoApp.Business.Services.Base;
using ToDoApp.Domain.Entity;

namespace ToDoApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class ToDoCategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ToDoCategoryController> _logger;

        private IToDoCategoryService _toDoCategoryService;
        public ToDoCategoryController(ILogger<ToDoCategoryController> logger,
            IMapper mapper,
            IToDoCategoryService toDoCategoryService)
        {
            _logger = logger;
            _mapper = mapper;
            _toDoCategoryService = toDoCategoryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get all categories")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _toDoCategoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Get category by id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _toDoCategoryService.GetById(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get categories by name or letters")]
        public async Task<IActionResult> GetByName(string name)
        {
            var categories = await _toDoCategoryService.GetByName(name);
            return Ok(categories);
        }

        [HttpGet("shortname/{shortname}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Get categories by shortname or letters")]
        public async Task<IActionResult> GetByShortName(string shortName)
        {
            var category = await _toDoCategoryService.GetByShortName(shortName);
            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [SwaggerOperation(Summary = "Create category")]
        public async Task<IActionResult> Create(ToDoCategory category)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                category.Id = Guid.NewGuid();
                await _toDoCategoryService.Create(category);
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
        [SwaggerOperation(Summary = "Update category")]
        public async Task<IActionResult> Update(ToDoCategory category)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _toDoCategoryService.Update(category);
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
        [SwaggerOperation(Summary = "Delete category")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var item = await _toDoCategoryService.GetById(id);
                if (item == null)
                    return BadRequest("No Category with a given id");

                await _toDoCategoryService.Remove(id);
                return Ok();
            }
            catch(Exception e)
            {
                _logger.LogError("Bad Request - {0}", e.Message);
                return BadRequest(e.Message);
            }           
        }
    }
}
