FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

COPY ApiTemplate.Build.sln ApiTemplate.Build.sln
COPY /Api/ApiTemplate.Api/ApiTemplate.Api.csproj /Api/ApiTemplate.Api/ApiTemplate.Api.csproj
COPY /Api/ApiTemplate/ApiTemplate.csproj /Api/ApiTemplate/ApiTemplate.csproj
COPY /Api/ApiTemplate.Infrastructure/ApiTemplate.Infrastructure.csproj /Api/ApiTemplate.Infrastructure/ApiTemplate.Infrastructure.csproj

COPY nuget.config nuget.config
RUN dotnet restore "ApiTemplate.Build.sln"

COPY . .
WORKDIR /Api/ApiTemplate.Api
RUN dotnet publish --no-restore -c Docker -o ./out
ENTRYPOINT ["dotnet", "./out/ApiTemplate.Api.dll"]

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /Api/ApiTemplate.Api/out .
EXPOSE 5000
ENTRYPOINT ["dotnet", "ApiTemplate.Api.dll"]
