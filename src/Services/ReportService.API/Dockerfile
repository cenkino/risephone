#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/ReportService.API/ReportService.API.csproj", "Services/ReportService.API/"]
COPY ["EventBus/EventBus.Base/EventBus.Base.csproj", "EventBus/EventBus.Base/"]
COPY ["EventBus/EventBus.Factory/EventBus.Factory.csproj", "EventBus/EventBus.Factory/"]
COPY ["EventBus/EventBus.RabbitMQ/EventBus.RabbitMQ.csproj", "EventBus/EventBus.RabbitMQ/"]
COPY ["Shared/RisePhoneApp.Shared/RisePhoneApp.Shared.csproj", "Shared/RisePhoneApp.Shared/"]
RUN dotnet restore "./Services/ReportService.API/./ReportService.API.csproj"
COPY . .
WORKDIR "/src/Services/ReportService.API"
RUN dotnet build "./ReportService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ReportService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportService.API.dll"]