# Introduction 
-	The TODO HTTP API lets a client create, read, update and delete to-do tasks
That RESTful API contains JwtAuthentication , versioning , integration tests.

# Getting Started
API contains register and logging of user. As you start you must register new account with email and password </br>
"Passwords must have at least one non alphanumeric character.", </br>
"Passwords must have at least one digit ('0'-'9').",</br>
"Passwords must have at least one uppercase ('A'-'Z')."

CustomTasks methods are opened only for logged users. 
Each user has his personal tasks , so you can update and delete only tasks that you have created.

# Build and Test
TODO: To start application just clone repository and follow steps:
1.	Go to ToDoListRESTfulApi-Levi9TechnicalTask\TasksListAPI folder
2.	Open command line and run command: 
```
dotnet run 
```
3.	So now you can access to OpenAPI documentation
4.	OpenAPI:  https://localhost:5001/swagger/index.html
### Start with Visual Studio
Please change IIS Express to TasksListAPI.
----
## Connection string
API work with mssql and localdb but you can change connection string to appropriate for you.
```
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TasksListAPI;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
```
## CosmoDB as a bonus feature
Also I provide CosmoDb support. But if you want to use it , you must delete some comments in code to change database.
In future I will provide API with Factory in reason to change database easier.
----
# Entity Framework Core In Memory Testing database
When developing Unit or Integration Tests you don’t want to use a physical database on a server, because you don’t necessarily 
want to concern yourself with ceremony or administration tasks of maintaining the database server. </br>
So I used Microsoft.EntityFrameworkCore.InMemory to create TestDb in memory. </br>
All tests passed.