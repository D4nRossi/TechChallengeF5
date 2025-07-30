FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["menu-service.csproj", "./"]
RUN dotnet restore "menu-service.csproj"
COPY . .
RUN dotnet build "menu-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "menu-service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "menu-service.dll"]
