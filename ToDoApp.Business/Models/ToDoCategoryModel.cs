using System;

namespace ToDoApp.Business.Models
{
    public class ToDoCategoryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}