FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Orders/Orders.API/Orders.API.csproj", "Orders/Orders.API/"]
COPY ["Orders/Orders.Domain/Orders.Domain.csproj", "Orders/Orders.Domain/"]
COPY ["Orders/Orders.Application/Orders.Application.csproj", "Orders/Orders.Application/"]
COPY ["Orders/Orders.Infrastructure/Orders.Infrastructure.csproj", "Orders/Orders.Infrastructure/"]
RUN dotnet restore "Orders/Orders.API/Orders.API.csproj"
COPY . .
WORKDIR "/src/Orders/Orders.API"
RUN dotnet build "Orders.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Orders.API.csproj" -c Release -o /app/publish /p:PublishAot=true /p:UseAppHost=false /p:RuntimeIdentifier=linux-x64

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Orders.API.dll"]
