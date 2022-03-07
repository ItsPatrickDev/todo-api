using System;

namespace ToDoApp.Business.Models
{
    public class SubTaskModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public Guid ToDoItemId { get; set; }
    }
}