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
using ToDoApp.Tests.Services;
using ToDoApp.WebApi.Controllers;
using Xunit;

namespace ToDoApp.Tests.Controllers
{
    public class ToDoCategoryControllerTest
    {
        private static IMapper _mapper;
        private IToDoCategoryService _toDoCategoryService;
        private ToDoCategoryController controller;
        public ToDoCategoryControllerTest()
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
            _toDoCategoryService = new ToDoCategoryServiceFake(_mapper);
            controller = new ToDoCategoryController(_mapper, _toDoCategoryService);
        }

        // GETALL
        [Fact]
        public async Task GetAll_ReturnsOkResult()
        {
            var okResult = await controller.GetAll();

            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetAll_WhenCalled_ReturnsAllItems()
        {
            var okResult = await controller.GetAll() as OkObjectResult;

            var items = Assert.IsType<List<ToDoCategoryModel>>(okResult.Value);
            Assert.Equal(2, items.Count);
        }

        // GETBYID
        [Fact]
        public async Task GetById_UknownGuid_ReturnsNotFound()
        {
            var testGuid = Guid.NewGuid();

            var notFoundResult = await controller.GetById(testGuid);

            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public async Task GetById_ExistingGuid_ReturnsNotFound()
        {
            var testGuid = new Guid("00000000-1111-1111-1111-000000000000");

            var okObjectResult = await controller.GetById(testGuid);

            Assert.IsType<OkObjectResult>(okObjectResult);
        }

        // GETBYNAME
        [Fact]
        public async Task GetByName_NoExistingName_ReturnsEmptyResult()
        {
            var testName = "Test";

            var result = await controller.GetByName(testName) as OkObjectResult;

            var categories = Assert.IsType<List<ToDoCategoryModel>>(result.Value);
            Assert.Empty(categories);
        }

        [Fact]
        public async Task GetByName_NoExistingName_ReturnsResult()
        {
            var testName = "Home";

            var result = await controller.GetByName(testName) as OkObjectResult;

            var categories = Assert.IsType<List<ToDoCategoryModel>>(result.Value);
            Assert.Single(categories);
        }

        // GETBYSHORTNAME
        [Fact]
        public async Task GetByShortName_NoExistingName_ReturnsEmptyResult()
        {
            var testShortName = "QQ";

            var result = await controller.GetByShortName(testShortName) as OkObjectResult;

            var categories = Assert.IsType<List<ToDoCategoryModel>>(result.Value);
            Assert.Empty(categories);
        }

        [Fact]
        public async Task GetByShortName_NoExistingName_ReturnsResult()
        {
            var testShortName = "HM";

            var result = await controller.GetByShortName(testShortName) as OkObjectResult;

            var categories = Assert.IsType<List<ToDoCategoryModel>>(result.Value);
            Assert.Single(categories);
        }

        // CREATE
        [Fact]
        public async Task Create_InvalidModel_ReturnsBadRequest()
        {
            ToDoCategory category = null;

            var result = await controller.Create(category);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Create_ValidModel_ReturnsOkResult()
        {
            var category = new ToDoCategory()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                ShortName = "TT"
            };

            var result = await controller.Create(category);

            Assert.IsType<OkResult>(result);
        }

        // UPDATE
        [Fact]
        public async Task Update_InvalidModel_ReturnsBadRequest()
        {
            ToDoCategory category = null;

            var result = await controller.Update(category);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Update_ValidModel_ReturnsBadRequest()
        {
            var category = new ToDoCategory()
            {
                Id = new Guid("00000000-1111-1111-1111-000000000000"),
                Name = "Test",
                ShortName = "HM"
            };

            var result = await controller.Update(category);

            Assert.IsType<OkResult>(result);
        }

        // DELETE
        [Fact]
        public async Task Delete_ReturnsOkResult()
        {
            var categoryId = new Guid("00000000-1111-1111-1111-000000000000");

            var result = await controller.Delete(categoryId);

            Assert.IsType<OkResult>(result);
        }
    }
}
