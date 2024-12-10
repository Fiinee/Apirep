from mcr.microsoft.com/dotnet/aspnet:8.0 as base

expose 80
env ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app
from mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

Copy ["WebApplication2/WebApplication2.csproj","WebApplication2/"]
copy ["DataAccess/DataAccess.csproj", "DataAccess/"]
copy ["ConsoleApp1/ConsoleApp1.csproj", "ConsoleApp1/"]
run dotnet restore "WebApplication2/WebApplication2.csproj"

copy . . 
from build as publish
run dotnet publish "WebApplication2/WebApplication2.csproj" -c Release -o /app/publish /p:UseAppHost=false

from base as final
WORKDIR /app
copy --from=publish /app/publish .
entrypoint ["dotnet", "WebApplication2.dll"]