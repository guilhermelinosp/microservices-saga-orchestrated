FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Stocks/Stocks.API/Stocks.API.csproj", "Stocks/Stocks.API/"]
COPY ["Stocks/Stocks.Domain/Stocks.Domain.csproj", "Stocks/Stocks.Domain/"]
COPY ["Stocks/Stocks.Application/Stocks.Application.csproj", "Stocks/Stocks.Application/"]
COPY ["Stocks/Stocks.Infrastructure/Stocks.Infrastructure.csproj", "Stocks/Stocks.Infrastructure/"]
RUN dotnet restore "Stocks/Stocks.API/Stocks.API.csproj"
COPY . .
WORKDIR "/src/Stocks/Stocks.API"
RUN dotnet build "Stocks.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Stocks.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stocks.API.dll"]
