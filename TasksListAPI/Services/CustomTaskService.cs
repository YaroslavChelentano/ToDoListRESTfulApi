using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksListAPI.Data;
using TasksListAPI.Domain;

namespace TasksListAPI.Services
{
    public class CustomTaskService : ICustomTaskService
    {
        private readonly DataContext _dataContext;

        public CustomTaskService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<CustomTask>> GetCustomTasksAsync()
        {
            return await _dataContext.CustomTasks.ToListAsync();
        }

        public async Task<CustomTask> GetCustomTaskByTitleAsync(string customTaskTitle)
        {
            return await _dataContext.CustomTasks.SingleOrDefaultAsync(x => x.Title == customTaskTitle);
        }

        public async Task<bool> CreateCustomTaskAsync(CustomTask customTask)
        {
            await _dataContext.CustomTasks.AddAsync(customTask);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0; 
        }

        public async Task<bool> UpdateCustomTaskAsync(CustomTask customTaskToUpdate)
        {
            _dataContext.CustomTasks.Update(customTaskToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteCustomTaskAsync(string customTaskTitle)
        {
            var customTask =await GetCustomTaskByTitleAsync(customTaskTitle);

            if (customTask == null)
                return false;

            _dataContext.CustomTasks.Remove(customTask);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> UserOwnsCustomTaskAsync(string customTaskTitle, string userId)
        {
            var customTask =await _dataContext.CustomTasks.AsNoTracking().SingleOrDefaultAsync(x => x.Title == customTaskTitle);

            if (customTask == null)
                return false;

            if (customTask.UserId != userId)
                return false;

            return true;
        }
    }
}
