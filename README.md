## DotNet WebApp Doc

##CMDS


```
dotnet tool install --global dotnet-ef
```

<!-- create app -->
# Create Web App using dot_net
```
dotnet new webapi -n DotNet9CookieAuthAPI
cd DotNet9CookieAuthAPI
```
<!-- update db -->
# update to the db
The command dotnet ef migrations add InitialCreate is used in Entity Framework Core (EF Core) to create a new migration. Here's what it does in detail:
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

<!-- this is how to remove files in git -->
# this is how to remove files in git
```
git rm --cached appsettings.json
```
