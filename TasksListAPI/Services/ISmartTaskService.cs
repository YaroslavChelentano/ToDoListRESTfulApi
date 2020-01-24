using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksListAPI.Domain;

namespace TasksListAPI.Services
{
    public interface ISmartTaskService
    {
        Task<List<SmartTask>> GetSmartTasksAsync();

        Task<SmartTask> GetSmartTaskByTitleAsync(string smartTaskTitle);

        Task<bool> CreateSmartTaskAsync(SmartTask smartTask);

        Task<bool> UpdateSmartTaskAsync(SmartTask smartTaskToUpdate);

        Task<bool> DeleteSmartTaskAsync(string smartTaskTitle);

        Task<bool> UserOwnsSmartTaskAsync(string smartTaskTitle, string userId);
    }
}
