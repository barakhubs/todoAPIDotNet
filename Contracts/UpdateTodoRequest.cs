using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI.Contracts
{
    public class UpdateTodoRequest
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public bool IsComplete { get; set; }

        [Required]
        public DateTime? DueDate { get; set; }

        [Range(1, 5)]
        public int? Priority { get; set; }

        public UpdateTodoRequest()
        {
            IsComplete = false;
        }
    }
}