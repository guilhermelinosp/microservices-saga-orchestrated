FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Stocks/Stocks.API/Stocks.API.csproj", "Stocks/Stocks.API/"]
COPY ["Stocks/Stocks.Domain/Stocks.Domain.csproj", "Stocks/Stocks.Domain/"]
COPY ["Stocks/Stocks.Application/Stocks.Application.csproj", "Stocks/Stocks.Application/"]
COPY ["Stocks/Stocks.Infrastructure/Stocks.Infrastructure.csproj", "Stocks/Stocks.Infrastructure/"]
RUN dotnet restore "Stocks/Stocks.API/Stocks.API.csproj"
COPY . .
WORKDIR "/src/Stocks/Stocks.API"
RUN dotnet build "Stocks.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN apt-get update && apt-get install -y clang
RUN dotnet publish "Stocks.API.csproj" -c Release -o /app/publish /p:PublishAot=true /p:UseAppHost=false /p:RuntimeIdentifier=linux-x64

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stocks.API.dll"]
