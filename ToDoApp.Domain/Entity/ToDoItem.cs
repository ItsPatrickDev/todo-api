using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Domain.Enums;

namespace ToDoApp.Domain.Entity
{
    public class ToDoItem : BaseEntity
    {
        [Required(AllowEmptyStrings = false)]
        [MinLength(3)]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = true)]
        [MaxLength(256)]
        public string Description { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
        public ToDoCategory Category { get; set; }

        [Required]
        public ToDoPriority Priority { get; set; }
        public ICollection<SubTask> SubTasks { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
