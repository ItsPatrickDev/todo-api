using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Business.Mappers;
using ToDoApp.Business.Services;
using ToDoApp.Business.Services.Base;
using ToDoApp.Data.Repositories;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.WebApi.Configuration
{
    public static class DependencyConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IToDoItemService, ToDoItemService>();
            services.AddTransient<IToDoCategoryService, ToDoCategoryService>();
            services.AddTransient<IToDoSubTaskService, ToDoSubTaskService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IToDoItemRepository, ToDoItemRepository>();
            services.AddTransient<IToDoCategoryRepository, ToDoCategoryRepository>();
            services.AddTransient<ISubTaskRepository, SubTaskRepository>();
        }

        public static void AddMappers(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
