using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TasksListAPI.Contracts.V1;
using TasksListAPI.Contracts.V1.Requests;
using TasksListAPI.Contracts.V1.Responses;
using TasksListAPI.Domain;
using TasksListAPI.Extensions;
using TasksListAPI.Services;

namespace TasksListAPI.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomTaskController : Controller
    {
        private readonly ICustomTaskService _customTaskService;
        public CustomTaskController(ICustomTaskService customTaskService)
        {
            _customTaskService = customTaskService;
        }

        [HttpGet(ApiRoutes.CustomTask.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _customTaskService.GetCustomTasksAsync());
        }

        [HttpGet(ApiRoutes.CustomTask.GetActiveTasks)]
        public async Task<IActionResult> GetActiveTasks()
        {
            var customTasks = await _customTaskService.GetCustomTasksAsync();
            var customActiveTasks = customTasks.Where(x => x.IsCompleted == false);

            return Ok(customActiveTasks);
        }

        [HttpGet(ApiRoutes.CustomTask.Get)]
        public async Task<IActionResult> Get([FromRoute] string customTaskTitle)
        {
            var customTask =await _customTaskService.GetCustomTaskByTitleAsync(customTaskTitle);

            if (customTask == null)
                return NotFound();

            return Ok(customTask);
        }

        [HttpPut(ApiRoutes.CustomTask.Update)]
        public async Task<IActionResult> Update([FromRoute] string customTaskTitle, [FromBody] UpdateCustomTaskRequest request)
        {
            var userOwnsCustomTask = await _customTaskService.UserOwnsCustomTaskAsync(customTaskTitle, HttpContext.GetUserId());

            if(!userOwnsCustomTask)
            {
                return BadRequest(new { error = "You don't own this customTask" });
            }

            var customTask = await _customTaskService.GetCustomTaskByTitleAsync(customTaskTitle);
            customTask.Title = request.Title;
            customTask.Description = request.Description;
            customTask.Importance = request.Importance;
            customTask.DueDate = request.DueDate;
            customTask.IsCompleted = request.IsCompleted;


            var updated =await _customTaskService.UpdateCustomTaskAsync(customTask);

            if(updated)
                return Ok(customTask);

            return NotFound();
        }

        [HttpDelete(ApiRoutes.CustomTask.Delete)]
        public async Task<IActionResult> Delete ([FromRoute] string customTaskTitle)
        {
            var userOwnsCustomTask = await _customTaskService.UserOwnsCustomTaskAsync(customTaskTitle, HttpContext.GetUserId());

            if (!userOwnsCustomTask)
            {
                return BadRequest(new { error = "You don't own this customTask" });
            }

            var deleted =await _customTaskService.DeleteCustomTaskAsync(customTaskTitle);

            if (deleted)
                return NoContent();

            return NotFound();
        }

        [HttpDelete(ApiRoutes.CustomTask.MultipleDelete)]
        public async Task<IActionResult> MultipleDelete([FromRoute] string customTaskTitle)
        {
            var multipleTasks = customTaskTitle.Split(new char[] { ',' });
            for (int i = 0; i < multipleTasks.Length-1; i++)
            {
                var deleted = await _customTaskService.DeleteCustomTaskAsync(multipleTasks[i].ToString());
                if (deleted)
                    return NoContent();
            }

            return NotFound();
        }

        [HttpPost(ApiRoutes.CustomTask.Create)]
        public async Task<IActionResult> Create([FromBody] CreateCustomTaskRequest customTaskRequest)
        {
            var customTask = new CustomTask { 
                Title = customTaskRequest.Title,
                Description = customTaskRequest.Description,
                Importance = customTaskRequest.Importance,
                DueDate = customTaskRequest.DueDate,
                IsCompleted = customTaskRequest.IsCompleted,
                UserId = HttpContext.GetUserId()
            };

            await _customTaskService.CreateCustomTaskAsync(customTask);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.CustomTask.Get.Replace("customTaskTitle", customTask.Title);

            var response = new CustomTaskResponse { Title = customTask.Title , Description=customTask.Description, 
                Importance=customTask.Importance, DueDate=customTask.DueDate, IsCompleted= customTask.IsCompleted };
            return Created(locationUri, response);
        }
    }
}
