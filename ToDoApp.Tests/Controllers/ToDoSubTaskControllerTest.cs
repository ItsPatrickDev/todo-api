using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Business.Mappers;
using ToDoApp.Business.Models;
using ToDoApp.Business.Models.SubTask;
using ToDoApp.Business.Services.Base;
using ToDoApp.Domain.Entity;
using ToDoApp.Tests.Services;
using ToDoApp.WebApi.Controllers;
using Xunit;

namespace ToDoApp.Tests.Controllers
{
    public class ToDoSubTaskControllerTest
    {
        private static IMapper _mapper;
        private IToDoSubTaskService _toDoSubTaskervice;
        private ToDoSubTaskController controller;
        public ToDoSubTaskControllerTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _toDoSubTaskervice = new ToDoSubTaskServiceFake(_mapper);
            controller = new ToDoSubTaskController(_mapper, _toDoSubTaskervice);
        }

        // GETALLSUBTASKS
        [Fact]
        public async Task GetAllSubtasks_ExistingId_ReturnsOkResult()
        {
            var testGuid = new Guid("11111111-1111-1111-1111-111111111111");

            var result = await controller.GetAllSubtasks(testGuid) as OkObjectResult;

            var items = Assert.IsType<List<SubTaskModel>>(result.Value);
            Assert.Single(items);
        }

        [Fact]
        public async Task GetAllSubtasks_NoExistingId_ReturnsOkResult()
        {
            var testGuid = Guid.NewGuid();

            var result = await controller.GetAllSubtasks(testGuid) as OkObjectResult;

            var items = Assert.IsType<List<SubTaskModel>>(result.Value);
            Assert.Empty(items);
        }

        // CREATE
        [Fact]
        public async Task Create_ValidModel_ReturnsBadRequestResult()
        {
            CreateSubTaskModel createSubTask = new CreateSubTaskModel()
            {
                Name = "Test",
                Description = "Test",
                ToDoItemId = Guid.NewGuid()
            };

            var result = await controller.Create(createSubTask);

            Assert.IsType<OkResult>(result);
        }

        // SETDONE
        [Fact]
        public async Task SetDone_NoExistingId_ReturnsBadRequestResult()
        {
            var subTaskId = Guid.NewGuid();

            var result = await controller.SetDone(subTaskId);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task SetDone_ExistingId_ReturnsBadRequestResult()
        {
            var subTaskId = new Guid("00000000-1111-0000-1111-000000000000");

            var result = await controller.SetDone(subTaskId);

            Assert.IsType<OkResult>(result);
        }
    }
}
