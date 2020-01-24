using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksListAPI.Domain;

namespace TasksListAPI.Services
{
    public interface ICustomTaskService
    {
        Task<List<CustomTask>> GetCustomTasksAsync();

        Task<CustomTask> GetCustomTaskByTitleAsync(string customTaskTitle);

        Task<bool> CreateCustomTaskAsync(CustomTask customTask);

        Task<bool> UpdateCustomTaskAsync(CustomTask customTaskToUpdate);

        Task<bool> DeleteCustomTaskAsync(string customTaskTitle);

        Task<bool> UserOwnsCustomTaskAsync(string customTaskTitle, string userId);
    }
}
