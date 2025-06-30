# Etapa 1: Build con .NET 8 SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar solución y proyecto
COPY MyChamba.sln .
COPY MyChamba.API/*.csproj ./MyChamba.API/

# Restaurar dependencias
RUN dotnet restore ./MyChamba.API/MyChamba.API.csproj

# Copiar el resto del código
COPY MyChamba.API/. ./MyChamba.API/

# Publicar en modo Release
WORKDIR /app/MyChamba.API
RUN dotnet publish -c Release -o /out

# Etapa 2: Runtime con imagen más ligera
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# Configurar puerto esperado por Railway
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Arrancar la app
ENTRYPOINT ["dotnet", "MyChamba.API.dll"]
