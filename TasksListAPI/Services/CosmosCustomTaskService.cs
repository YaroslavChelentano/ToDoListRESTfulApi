using Cosmonaut;
using Cosmonaut.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksListAPI.Domain;

namespace TasksListAPI.Services
{
    public class CosmosCustomTaskService : ICustomTaskService
    {
        private readonly ICosmosStore<CosmosCustomTaskDto> _cosmosStore;

        public CosmosCustomTaskService(ICosmosStore<CosmosCustomTaskDto> cosmosStore)
        {
            _cosmosStore = cosmosStore;
        }

        public async Task<bool> CreateCustomTaskAsync(CustomTask customTask)
        {
            var cosmosCustomTask = new CosmosCustomTaskDto
            {
                Title = customTask.Title,
                Description = customTask.Description
            };

            var response = await _cosmosStore.AddAsync(cosmosCustomTask);
            customTask.Title = cosmosCustomTask.Title;
            return response.IsSuccess;
        }

        public async Task<bool> DeleteCustomTaskAsync(string customTaskTitle)
        {
            var customTask = await _cosmosStore.FindAsync(customTaskTitle, customTaskTitle);

            var response = await _cosmosStore.RemoveAsync(customTask);

            return response.IsSuccess;
        }

        public async Task<CustomTask> GetCustomTaskByTitleAsync(string customTaskTitle)
        {
            var customTask = await _cosmosStore.FindAsync(customTaskTitle, customTaskTitle);

            return customTask == null ? null : new CustomTask { Title = customTask.Title, Description = customTask.Description };
        }

        public async Task<List<CustomTask>> GetCustomTasksAsync()
        {
            var customTasks = await _cosmosStore.Query().ToListAsync();

            return customTasks.Select(x => new CustomTask { Title = x.Title, Description = x.Description }).ToList();
        }

        public async Task<bool> UpdateCustomTaskAsync(CustomTask customTaskToUpdate)
        {
            var cosmosCustomTask = new CosmosCustomTaskDto
            {
                Title = customTaskToUpdate.Title,
                Description = customTaskToUpdate.Description
            };

            var response = await _cosmosStore.UpdateAsync(cosmosCustomTask);
            return response.IsSuccess;
        }

        public async Task<bool> UserOwnsCustomTaskAsync(string customTaskTitle, string userId)
        {
            var customTask = await _cosmosStore.FindAsync(customTaskTitle, customTaskTitle);

            if (customTask == null)
                return false;

            if (customTask.UserId != userId)
                return false;

            return true;
        }
    }
}
