# TestingMvc
Asp.Net Testing Website

A website for test cases, test results etc.

It is an Asp.Net Mvc4 project.
The database can be accessed using Entity Framework 6.

Currently it is designed to use Postgres, but the code should be database agnostic. 
If the same tables exist and the Web.Config is updated with new connection info, then it should work.

To run the website.
1. Add the tables to the database
2. Edit the Web.Config file with the new connection details.
3. Run the project in Mono Develop or Visual Studio.
4. Manually call the /Home/AddAdminUser which will add a new User "Admin". (See Home controller for details).


Database - Nuget Packages Required:
Entity Framework 6.0
Npgsql 2.2.3
Npgsql.EntityFramework 2.2.3

TestingMvc - Nuget Packages Required:
Entity Framework 6.0
Npgsql 2.2.3
Npgsql.EntityFramework 2.2.3
NauckIT.PostgreSQLProvider 2.0.0
Microsoft.AspNet.Mvc 4.0.2
Microsoft.AspNet.Razor 2.0
Microsoft.AspNet.WebApi 4.0
Microsoft.AspNet.WebPages 2.0
Microsoft.Net.Http 2.0
Microsoft.Web.Infrastructure 2.0
