using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Business.Mappers;
using ToDoApp.Business.Models;
using ToDoApp.Business.Services.Base;
using ToDoApp.Domain.Entity;
using ToDoApp.Domain.Enums;
using ToDoApp.Tests.Services;
using ToDoApp.WebApi.Controllers;
using Xunit;

namespace ToDoApp.Tests.Controllers
{
    public class ToDoItemsControllerTest
    {
        private static IMapper _mapper;
        private IToDoItemService _toDoItemService;
        private ToDoItemsController controller;
        public ToDoItemsControllerTest()
        {
            if(_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _toDoItemService = new ToDoItemServiceFake(_mapper);
            controller = new ToDoItemsController(_mapper, _toDoItemService);

        }

        // GETALL
        [Fact]
        public async Task GetAll_ReturnsOkResult()
        {
            var okObjectResult = await controller.GetAll();

            Assert.IsType<OkObjectResult>(okObjectResult);
        }

        [Fact]
        public async Task GetAll_WhenCalled_ReturnsAllItems()
        {
            var result = await controller.GetAll() as OkObjectResult;

            var items = Assert.IsType<List<ToDoItemModel>>(result.Value);
            Assert.Equal(3, items.Count);
        }


        // GETBYID
        [Fact]
        public async Task GetById_NoExistingGuid_ReturnsNotFoundResult()
        {
            var notFoundResult = await controller.GetById(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public async Task GetById_ExistingGuid_ReturnsOkResult()
        {
            var testGuid = new Guid("11111111-1111-1111-1111-111111111111");

            var okObjectResult = await controller.GetById(testGuid);

            Assert.IsType<OkObjectResult>(okObjectResult);
        }

        //GETBYNAME
        [Fact]
        public async Task GetByName_NoExistingName_ReturnsEmptyResult()
        {
            var testName = "Empty";

            var result = await controller.GetByName(testName) as OkObjectResult;

            var items = Assert.IsType<List<ToDoItemModel>>(result.Value);
            Assert.Empty(items);
        }

        [Fact]
        public async Task GetByName_ExistingName_ReturnsOkResult()
        {
            var testName = "Study";

            var result = await controller.GetByName(testName) as OkObjectResult;

            var items = Assert.IsType<List<ToDoItemModel>>(result.Value);
            Assert.Single(items);
        }

        // GETBYCATEGORY
        [Fact]
        public async Task GetByCategory_NoExistingCategory_ReturnsEmptyResult()
        {
            var testCategoryId = new Guid("22000222-3003-3003-3003-222222220002");

            var result = await controller.GetByCategory(testCategoryId) as OkObjectResult;

            var items = Assert.IsType<List<ToDoItemModel>>(result.Value);
            Assert.Empty(items);
        }

        [Fact]
        public async Task GetByCategory_ExistingCategory_ReturnsOkResult()
        {
            var testCategoryId = new Guid("22222222-3333-3333-3333-222222222222");

            var result = await controller.GetByCategory(testCategoryId) as OkObjectResult;

            var items = Assert.IsType<List<ToDoItemModel>>(result.Value);
            Assert.Equal(2, items.Count);
        }

        // GETBYPRIORITY
        [Fact]
        public async Task GetByPriority_NoExistingPriority_ReturnsEmptyResult()
        {
            var testPriority = ToDoPriority.Low;

            var result = await controller.GetByPriority(testPriority) as OkObjectResult;

            var items = Assert.IsType<List<ToDoItemModel>>(result.Value);
            Assert.Empty(items);
        }

        [Fact]
        public async Task GetByPriority_ExistingPriority_ReturnsOkResult()
        {
            var testPriority = ToDoPriority.Medium;

            var result = await controller.GetByPriority(testPriority) as OkObjectResult;

            var items = Assert.IsType<List<ToDoItemModel>>(result.Value);
            Assert.Equal(2, items.Count);
        }

        // CREATE
        [Fact]
        public async Task Create_InvalidModel_ReturnsBadRequestResult()
        {
            ToDoItem testToDoModel = null;

            var result = await controller.Create(testToDoModel);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Create_ModelWithExistingName_ReturnsBadRequestResult()
        {
            var testToDoModel = new ToDoItem()
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "Clean up house",
                Description = "All rooms",
                CategoryId = new Guid("11111111-0000-0000-0000-111111111111"),
                Category = new ToDoCategory()
                {
                    Id = new Guid("00000000-1111-1111-1111-000000000000"),
                    Name = "Home",
                    ShortName = "HM"
                },
                Priority = ToDoPriority.High,
                SubTasks = new List<SubTask>(),
                CreationDate = DateTime.Now,
                EndDate = new DateTime(2022, 3, 5)
            };

            var result = await controller.Create(testToDoModel);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Create_NewTODO_ReturnsOkRequestResult()
        {
            var testToDoModel = new ToDoItem()
            {
                Id = new Guid("77777777-7777-7777-7777-777777777777"),
                Name = "Testowy",
                Description = "All rooms",
                CategoryId = new Guid("11111111-0000-0000-0000-111111111111"),
                Category = new ToDoCategory()
                {
                    Id = new Guid("00000000-1111-1111-1111-000000000000"),
                    Name = "Home",
                    ShortName = "HM"
                },
                Priority = ToDoPriority.Urgent,
                SubTasks = new List<SubTask>(),
                CreationDate = DateTime.Now,
                EndDate = new DateTime(2022, 3, 5)
            };

            var result = await controller.Create(testToDoModel);

            Assert.IsType<OkResult>(result);
        }

        // UPDATE
        [Fact]
        public async Task Update_InvalidModel_ReturnsBadRequestResult()
        {
            ToDoItem testToDoModel = null;

            var result = await controller.Update(testToDoModel);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Update_ValidModel_ReturnsBadRequestResult()
        {
            ToDoItem testToDoModel = new ToDoItem()
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "Clean up house (UPDATE)",
                Description = "All rooms",
                CategoryId = new Guid("11111111-0000-0000-0000-111111111111"),
                Category = new ToDoCategory()
                {
                    Id = new Guid("00000000-1111-1111-1111-000000000000"),
                    Name = "Home",
                    ShortName = "HM"
                },
                Priority = ToDoPriority.High,
                SubTasks = new List<SubTask>()
                    {
                        new SubTask()
                        {
                            Id = new Guid("00000000-1111-0000-1111-000000000000"),
                            Name = "Clean windows",
                            Description = "All windows",
                            IsDone = false,
                            ToDoItemId = new Guid("11111111-1111-1111-1111-111111111111")
                        }
                    },
                CreationDate = DateTime.Now,
                EndDate = new DateTime(2022, 3, 5)
            };

            var result = await controller.Update(testToDoModel);

            Assert.IsType<OkResult>(result);
        }

        // DELETE
        [Fact]
        public async Task Delete_NotExistingId_ReturnsBadRequestResult()
        {
            var toDoId = Guid.NewGuid();

            var result = await controller.Delete(toDoId);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ExistingId_ReturnsOkRequestResult()
        {
            var toDoId = new Guid("11111111-1111-1111-1111-111111111111");

            var result = await controller.Delete(toDoId);

            Assert.IsType<OkResult>(result);
        }
    }
}
