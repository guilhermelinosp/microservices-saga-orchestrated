FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Orchestrators/Orchestrators.API/Orchestrators.API.csproj", "Orchestrators/Orchestrators.API/"]
COPY ["Orchestrators/Orchestrators.Domain/Orchestrators.Domain.csproj", "Orchestrators/Orchestrators.Domain/"]
COPY ["Orchestrators/Orchestrators.Application/Orchestrators.Application.csproj", "Orchestrators/Orchestrators.Application/"]
COPY ["Orchestrators/Orchestrators.Infrastructure/Orchestrators.Infrastructure.csproj", "Orchestrators/Orchestrators.Infrastructure/"]
RUN dotnet restore "Orchestrators/Orchestrators.API/Orchestrators.API.csproj"
COPY . .
WORKDIR "/src/Orchestrators/Orchestrators.API"
RUN dotnet build "Orchestrators.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Orchestrators.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Orchestrators.API.dll"]
