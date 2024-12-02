FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Inventories/Inventories.API/Inventories.API.csproj", "Inventories/Inventories.API/"]
COPY ["Inventories/Inventories.Domain/Inventories.Domain.csproj", "Inventories/Inventories.Domain/"]
COPY ["Inventories/Inventories.Application/Inventories.Application.csproj", "Inventories/Inventories.Application/"]
COPY ["Inventories/Inventories.Infrastructure/Inventories.Infrastructure.csproj", "Inventories/Inventories.Infrastructure/"]
RUN dotnet restore "Inventories/Inventories.API/Inventories.API.csproj"
COPY . .
WORKDIR "/src/Inventories/Inventories.API"
RUN dotnet build "Inventories.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Inventories.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inventories.API.dll"]
