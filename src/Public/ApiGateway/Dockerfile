#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Public/ApiGateway/ApiGateway.csproj", "src/Public/ApiGateway/"]
COPY ["src/Shared/RisePhoneApp.Shared/RisePhoneApp.Shared.csproj", "src/Shared/RisePhoneApp.Shared/"]
COPY ["src/EventBus/EventBus.Base/EventBus.Base.csproj", "src/EventBus/EventBus.Base/"]
RUN dotnet restore "./src/Public/ApiGateway/./ApiGateway.csproj"
COPY . .
WORKDIR "/src/src/Public/ApiGateway"
RUN dotnet build "./ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.dll"]