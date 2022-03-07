using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Domain.Entity
{
    public class ToDoCategory : BaseEntity
    {
        [Required]
        [MinLength(3)]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(8)]
        public string ShortName { get; set; }
    }
}
