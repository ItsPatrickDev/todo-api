using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Domain.Entity
{
    public class SubTask : BaseEntity
    {
        [Required]
        [MinLength(3)]
        [MaxLength(64)]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public bool IsDone { get; set; }
        public Guid ToDoItemId { get; set; }
    }
}
