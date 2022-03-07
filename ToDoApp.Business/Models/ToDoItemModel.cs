using System;
using System.Collections.Generic;
using ToDoApp.Domain.Entity;
using ToDoApp.Domain.Enums;

namespace ToDoApp.Business.Models
{
    public class ToDoItemModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public ToDoCategoryModel Category { get; set; }
        public ToDoPriority Priority { get; set; }
        public ICollection<SubTaskModel> SubTasks { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
