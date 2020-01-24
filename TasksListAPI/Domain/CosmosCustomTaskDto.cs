using Cosmonaut.Attributes;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TasksListAPI.Domain
{
    [CosmosCollection("customTasks")]
    public class CosmosCustomTaskDto
    {
        [CosmosPartitionKey]
        [JsonProperty("title")]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Importance { get; set; }

        public DateTime DueDate { get; set; }

        bool IsActive { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
    }
}
