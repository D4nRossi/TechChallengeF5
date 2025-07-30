FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["pedido-service.csproj", "./"]
RUN dotnet restore "pedido-service.csproj"
COPY . .
RUN dotnet build "pedido-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "pedido-service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "pedido-service.dll"]
