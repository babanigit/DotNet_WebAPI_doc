# DotNet_WebAPI_doc

cmds...

dotnet tool install --global dotnet-ef
dotnet tool install --global dotnet-ef

<!-- create app -->
dotnet new webapi -n DotNet9CookieAuthAPI
cd DotNet9CookieAuthAPI


dotnet ef migrations add InitialCreate

<!-- update db -->
dotnet ef database update


<!-- this is how to remove files in git -->
git rm --cached appsettings.json