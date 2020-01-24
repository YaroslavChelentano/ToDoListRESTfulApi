using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksListAPI.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public static class CustomTask
        {
            public const string GetAll = Base + "/tasks";

            public const string GetActiveTasks = Base + "/tasks/active";

            public const string Get = Base + "/tasks/{customTaskTitle}";

            public const string Update = Base + "/tasks/edit/{customTaskTitle}";

            public const string Delete = Base + "/tasks/{customTaskTitle}";

            public const string Create = Base + "/tasks/new";

            public const string MultipleDelete = Base + "/tasks/multiple/{customTaskTitle}";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";

            public const string Register = Base + "/identity/register";
        }

        public static class SmartTask
        {
            public const string GetAll = Base + "/smartTasks";

            public const string GetAllTasks = Base + "/smartTasks/allTasks";

            public const string GetPlannedTasks = Base + "/smartTasks/planned";

            public const string GetImportantTasks = Base + "/smartTasks/important";

            public const string GetTodaysTasks = Base + "/smartTasks/todays";

            public const string GetSortedByTitleTasks = Base + "/smartTasks/order_by=title&sort=asc";

            public const string GetSortedByTitleDescendingTasks = Base + "/smartTasks/order_by=title&sort=desc";

            public const string Get = Base + "/smartTasks/{smartTaskTitle}";

            public const string Update = Base + "/smartTasks/edit/{smartTaskTitle}";

            public const string Delete = Base + "/smartTasks/{smartTaskTitle}";

            public const string Create = Base + "/smartTasks/new";
        }

    }
}
