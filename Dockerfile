# Use a imagem oficial do .NET 8 SDK para build
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use a imagem oficial do .NET 8 SDK para construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["almb.csproj", "./"]
RUN dotnet restore "./almb.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "almb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "almb.csproj" -c Release -o /app/publish

# Configuração da imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "almb.dll"]
