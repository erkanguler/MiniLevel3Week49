# Asset tracker with data persistence, a console application

This is a very simple asset tracking application, Entity framework is used to persist data.

Before running the application do these:

1. Create a database first by running this script:
```
CreateDatabase_1.sql
```
2. Create an user for that database by running this script:
```
CreateUserForAssetsDatabase_2.sql
```
3. Intall these dependencies :
```
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.Extensions.Configuration.Json
```
4. Apply migration to Assets database by running this command:
```
dotnet ef database update
```

To run the application, use this command from CLI
 - dotnet run
