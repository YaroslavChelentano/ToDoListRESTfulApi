using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksListAPI.Extensions
{
    public static class GeneralExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if (httpContext == null)
            {
                return string.Empty;
            }
            return httpContext.User.Claims.Single(x => x.Type == "id").Value;
        }
    }
}
