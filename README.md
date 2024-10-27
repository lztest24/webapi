So, what is this?
=============
Just a simple .NET Core web API to retrieve products from an SQL Server database and edit their descriptions.

Cool, thanks!
=============

----------------------------------------

How to run
----------
Simply clone the repo and launch the `WebApi` project, e.g. using `dotnet watch run`. The Swagger page should be displayed shortly. If it is not, check build output for the URL the API is listening on and navigate to `<the://aforementioned:URL>/swagger` in your favorite browser.
> [!IMPORTANT]  
> You will most likely want to modify the connection string in `appsettings.json` with values pertinent to your SQL Server instance. The default one will certainly not work for you.

Note regarding test data
------------------------
The `WebApi` project contains an initial migration to seed the DB with randomized test data. If for some reason they are not to your liking, you may recreate them by running the following commands from inside the `WebApi` project folder:
```
dotnet ef database update 0
dotnet ef migrations remove
dotnet ef migrations add init
dotnet ef database update
```
