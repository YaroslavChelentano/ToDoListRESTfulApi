using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksListAPI.Contracts.V1.Requests
{
    public class UpdateCustomTaskRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Importance { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }
    }
}
