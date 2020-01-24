using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasksListAPI.Contracts.V1;
using TasksListAPI.Contracts.V1.Requests;
using TasksListAPI.Contracts.V1.Responses;
using TasksListAPI.Domain;
using TasksListAPI.Extensions;
using TasksListAPI.Services;

namespace TasksListAPI.Controllers.V1
{
    public class SmartTaskController : Controller
    {
        private readonly ISmartTaskService _smartTaskService;
        private readonly ICustomTaskService _customTaskService;

        public SmartTaskController(ISmartTaskService smartTaskService, ICustomTaskService customTaskService)
        {
            _smartTaskService = smartTaskService;
            _customTaskService = customTaskService;
        }
        [HttpGet(ApiRoutes.SmartTask.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _smartTaskService.GetSmartTasksAsync());
        }

        [HttpGet(ApiRoutes.SmartTask.GetAllTasks)]
        public async Task<IActionResult> GetAllTasks()
        {
            var smartTasks = await _smartTaskService.GetSmartTasksAsync();
            var customTasks = await _customTaskService.GetCustomTasksAsync();

            var allTasks = new List<object>();
            if (customTasks.Count() > 0)
                allTasks.Add(customTasks);

            if (smartTasks.Count() > 0)
                allTasks.Add(smartTasks);

            return Ok(allTasks);
        }

        [HttpGet(ApiRoutes.SmartTask.GetPlannedTasks)]
        public async Task<IActionResult> GetPlannedTasks()
        {
            var smartTasks = await _smartTaskService.GetSmartTasksAsync();
            var customTasks = await _customTaskService.GetCustomTasksAsync();
            var customPlannedTasks = customTasks.Where(x => x.DueDate > DateTime.Now);
            var smartPlannedTasks = smartTasks.Where(x => x.DueDate > DateTime.Now);
            var allTasks = new List<object>();

            if(customPlannedTasks.Count() > 0)
            allTasks.Add(customPlannedTasks);

            if(smartPlannedTasks.Count() > 0)
            allTasks.Add(smartPlannedTasks);

            return Ok(allTasks);
        }

        [HttpGet(ApiRoutes.SmartTask.GetTodaysTasks)]
        public async Task<IActionResult> GetTodaysTasks()
        {
            var smartTasks = await _smartTaskService.GetSmartTasksAsync();
            var customTasks = await _customTaskService.GetCustomTasksAsync();
            var customTodaysTasks = customTasks.Where(x => x.DueDate.Day == DateTime.Now.Day);
            var smartTodaysTasks = smartTasks.Where(x => x.DueDate.Day == DateTime.Now.Day);
            var allTasks = new List<object>();

            if(customTodaysTasks.Count() > 0)
            allTasks.Add(customTodaysTasks);

            if(smartTodaysTasks.Count() > 0)
            allTasks.Add(smartTodaysTasks);

            return Ok(allTasks);
        }

        [HttpGet(ApiRoutes.SmartTask.GetImportantTasks)]
        public async Task<IActionResult> GetImportantTasks()
        {
            var smartTasks = await _smartTaskService.GetSmartTasksAsync();
            var customTasks = await _customTaskService.GetCustomTasksAsync();
            var customImportantTasks = customTasks.Where(x => x.Importance.ToLower() == "low" || x.Importance.ToLower() == "normal" || x.Importance.ToLower() == "default" ||
            x.Importance.ToLower() == "high");
            var smartImportantTasks = smartTasks.Where(x => x.Importance.ToLower() == "low" || x.Importance.ToLower() == "normal" || x.Importance.ToLower() == "default" ||
            x.Importance.ToLower() == "high");
            var allTasks = new List<object>();

            if (customImportantTasks.Count() > 0)
                allTasks.Add(customImportantTasks);

            if (smartImportantTasks.Count() > 0)
                allTasks.Add(smartImportantTasks);

            return Ok(allTasks);
        }

        [HttpGet(ApiRoutes.SmartTask.Get)]
        public async Task<IActionResult> Get([FromRoute] string smartTaskTitle)
        {
            var smartTask = await _smartTaskService.GetSmartTaskByTitleAsync(smartTaskTitle);

            if (smartTask == null)
                return NotFound();

            return Ok(smartTask);
        }


        [HttpGet(ApiRoutes.SmartTask.GetSortedByTitleTasks)]
        public async Task<IActionResult> GetSortedByTitleTasks()
        {
            var smartTasks = await _smartTaskService.GetSmartTasksAsync();
            var orderedByTitleList = smartTasks.OrderBy(x => x.Title);

            return Ok(orderedByTitleList);
        }

        [HttpGet(ApiRoutes.SmartTask.GetSortedByTitleDescendingTasks)]
        public async Task<IActionResult> GetSortedByTitleDescendingTasks()
        {
            var smartTasks = await _smartTaskService.GetSmartTasksAsync();
            var orderedByTitleList = smartTasks.OrderByDescending(x => x.Title);

            return Ok(orderedByTitleList);
        }

        [HttpPut(ApiRoutes.SmartTask.Update)]
        public async Task<IActionResult> Update([FromRoute] string smartTaskTitle, [FromBody] UpdateSmartTaskRequest request)
        {
            var userOwnsCustomTask = await _smartTaskService.UserOwnsSmartTaskAsync(smartTaskTitle, HttpContext.GetUserId());

            if (!userOwnsCustomTask)
            {
                return BadRequest(new { error = "You don't own this smartTask" });
            }

            var smartTask = await _smartTaskService.GetSmartTaskByTitleAsync(smartTaskTitle);
            smartTask.Title = request.Title;
            smartTask.Description = request.Description;
            smartTask.Importance = request.Importance;
            smartTask.DueDate = request.DueDate;
            smartTask.IsCompleted = request.IsCompleted;


            var updated = await _smartTaskService.UpdateSmartTaskAsync(smartTask);

            if (updated)
                return Ok(smartTask);

            return NotFound();
        }
        /* client have not permission to delete SmartList
         
        [HttpDelete(ApiRoutes.SmartTask.Delete)]
        public async Task<IActionResult> Delete([FromRoute] string smartTaskTitle)
        {
            var userOwnsSmartTask = await _smartTaskService.UserOwnsSmartTaskAsync(smartTaskTitle, HttpContext.GetUserId());

            if (!userOwnsSmartTask)
            {
                return BadRequest(new { error = "You don't own this smartTask" });
            }

            var deleted = await _smartTaskService.DeleteSmartTaskAsync(smartTaskTitle);

            if (deleted)
                return NoContent();

            return NotFound();
        }
        */

        [HttpPost(ApiRoutes.SmartTask.Create)]
        public async Task<IActionResult> Create([FromBody] CreateCustomTaskRequest smartTaskRequest)
        {
            var smartTask = new SmartTask
            {
                Title = smartTaskRequest.Title,
                Description = smartTaskRequest.Description,
                Importance = smartTaskRequest.Importance,
                DueDate = smartTaskRequest.DueDate,
                IsCompleted = smartTaskRequest.IsCompleted,
                UserId = HttpContext.GetUserId()
            };

            await _smartTaskService.CreateSmartTaskAsync(smartTask);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.CustomTask.Get.Replace("smartTaskTitle", smartTask.Title);

            var response = new SmartTaskResponse
            {
                Title = smartTask.Title,
                Description = smartTask.Description,
                Importance = smartTask.Importance,
                DueDate = smartTask.DueDate,
                IsCompleted = smartTask.IsCompleted
            };
            return Created(locationUri, response);
        }
    }
}
