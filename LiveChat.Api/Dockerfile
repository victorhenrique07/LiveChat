#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LiveChat.Api/LiveChat.Api.csproj", "LiveChat.Api/"]
COPY ["LiveChat.Application/LiveChat.Application.csproj", "LiveChat.Application/"]
COPY ["LiveChat.Domain/LiveChat.Domain.csproj", "LiveChat.Domain/"]
COPY ["LiveChat.Infraestructure/LiveChat.Infraestructure.csproj", "LiveChat.Infraestructure/"]
RUN dotnet restore "./LiveChat.Api/LiveChat.Api.csproj"
COPY . .
WORKDIR "/src/LiveChat.Api"
RUN dotnet build "./LiveChat.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LiveChat.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LiveChat.Api.dll"]