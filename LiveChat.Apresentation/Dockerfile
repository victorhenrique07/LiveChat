FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY LiveChat.Apresentation/LiveChat.Apresentation.csproj LiveChat.Apresentation/
RUN dotnet restore LiveChat.Apresentation/LiveChat.Apresentation.csproj
COPY LiveChat.Apresentation/. LiveChat.Apresentation/
RUN dotnet publish LiveChat.Apresentation/LiveChat.Apresentation.csproj -c Release -o /app/publish

# Etapa 2: Imagem Final
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "LiveChat.Apresentation.dll"]