FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["FastTechFoods.Gateway.csproj", "./"]
RUN dotnet restore "FastTechFoods.Gateway.csproj"
COPY . .
RUN dotnet build "FastTechFoods.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FastTechFoods.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FastTechFoods.Gateway.dll"]
