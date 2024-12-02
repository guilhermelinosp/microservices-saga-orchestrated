FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Payments/Payments.API/Payments.API.csproj", "Payments/Payments.API/"]
COPY ["Payments/Payments.Domain/Payments.Domain.csproj", "Payments/Payments.Domain/"]
COPY ["Payments/Payments.Application/Payments.Application.csproj", "Payments/Payments.Application/"]
COPY ["Payments/Payments.Infrastructure/Payments.Infrastructure.csproj", "Payments/Payments.Infrastructure/"]
RUN dotnet restore "Payments/Payments.API/Payments.API.csproj"
COPY . .
WORKDIR "/src/Payments/Payments.API"
RUN dotnet build "Payments.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN apt-get update && apt-get install -y clang
RUN dotnet publish "Payments.API.csproj" -c Release -o /app/publish /p:PublishAot=true /p:UseAppHost=false /p:RuntimeIdentifier=linux-x64

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payments.API.dll"]
