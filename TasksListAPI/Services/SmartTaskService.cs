using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksListAPI.Data;
using TasksListAPI.Domain;

namespace TasksListAPI.Services
{
    public class SmartTaskService : ISmartTaskService
    {
        private readonly DataContext _dataContext;

        public SmartTaskService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<SmartTask>> GetSmartTasksAsync()
        {
            return await _dataContext.SmartTasks.ToListAsync();
        }

        public async Task<SmartTask> GetSmartTaskByTitleAsync(string SmartTaskTitle)
        {
            return await _dataContext.SmartTasks.SingleOrDefaultAsync(x => x.Title == SmartTaskTitle);
        }

        public async Task<bool> CreateSmartTaskAsync(SmartTask SmartTask)
        {
            await _dataContext.SmartTasks.AddAsync(SmartTask);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdateSmartTaskAsync(SmartTask SmartTaskToUpdate)
        {
            _dataContext.SmartTasks.Update(SmartTaskToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteSmartTaskAsync(string SmartTaskTitle)
        {
            var SmartTask = await GetSmartTaskByTitleAsync(SmartTaskTitle);

            if (SmartTask == null)
                return false;

            _dataContext.SmartTasks.Remove(SmartTask);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> UserOwnsSmartTaskAsync(string SmartTaskTitle, string userId)
        {
            var SmartTask = await _dataContext.SmartTasks.AsNoTracking().SingleOrDefaultAsync(x => x.Title == SmartTaskTitle);

            if (SmartTask == null)
                return false;

            if (SmartTask.UserId != userId)
                return false;

            return true;
        }
    }
}
