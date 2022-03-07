using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Business.Models;
using ToDoApp.Business.Models.SubTask;
using ToDoApp.Domain.Entity;

namespace ToDoApp.Business.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ToDoItem, ToDoItemModel>();
            CreateMap<ToDoItemModel, ToDoItem>();

            CreateMap<ToDoCategory, ToDoCategoryModel>();
            CreateMap<ToDoCategoryModel, ToDoCategory>();

            CreateMap<SubTask, SubTaskModel>();
            CreateMap<SubTaskModel, SubTask>();

            CreateMap<CreateSubTaskModel, SubTask>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(p => Guid.NewGuid()))
                .ForMember(dest => dest.IsDone, opt => opt.MapFrom(p => false));
        }
    }
}
