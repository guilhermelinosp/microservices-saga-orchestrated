FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Inventories/Inventories.API/Inventories.API.csproj", "Inventories/Inventories.API/"]
COPY ["Inventories/Inventories.Domain/Inventories.Domain.csproj", "Inventories/Inventories.Domain/"]
COPY ["Inventories/Inventories.Application/Inventories.Application.csproj", "Inventories/Inventories.Application/"]
COPY ["Inventories/Inventories.Infrastructure/Inventories.Infrastructure.csproj", "Inventories/Inventories.Infrastructure/"]
RUN dotnet restore "Inventories/Inventories.API/Inventories.API.csproj"
COPY . .
WORKDIR "/src/Inventories/Inventories.API"
RUN dotnet build "Inventories.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN apt-get update && apt-get install -y clang
RUN dotnet publish "Inventories.API.csproj" -c Release -o /app/publish /p:PublishAot=true /p:UseAppHost=false /p:RuntimeIdentifier=linux-x64

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inventories.API.dll"]
