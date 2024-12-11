from mcr.microsoft.com/dotnet/sdk:8.0 as build-env
workdir /app

copy . ./
run dotnet publish ./BlazorApp1.csproj -c Release -o output

from nginx:alpine
workdir /usr/share/nginx/html
copy --from=build-env /app/output/wwwroot .

copy nginx.conf /etc/nginx/nginx.conf

expose 80