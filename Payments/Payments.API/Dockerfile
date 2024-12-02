FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Payments/Payments.API/Payments.API.csproj", "Payments/Payments.API/"]
COPY ["Payments/Payments.Domain/Payments.Domain.csproj", "Payments/Payments.Domain/"]
COPY ["Payments/Payments.Application/Payments.Application.csproj", "Payments/Payments.Application/"]
COPY ["Payments/Payments.Infrastructure/Payments.Infrastructure.csproj", "Payments/Payments.Infrastructure/"]
RUN dotnet restore "Payments/Payments.API/Payments.API.csproj"
COPY . .
WORKDIR "/src/Payments/Payments.API"
RUN dotnet build "Payments.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Payments.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payments.API.dll"]
