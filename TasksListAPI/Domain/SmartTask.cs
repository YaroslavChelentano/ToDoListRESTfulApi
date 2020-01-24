using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TasksListAPI.Domain
{
    [Table("SmartTasks")]
    public class SmartTask
    {
        [Key]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Importance { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
    }
}
