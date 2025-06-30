# Etapa 1: Build con .NET 8 SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar solución y proyectos
COPY MyChamba.sln .
COPY MyChamba.API/*.csproj ./MyChamba.API/
COPY MyChamba.Application/*.csproj ./MyChamba.Application/
COPY MyChamba.Domain/*.csproj ./MyChamba.Domain/
COPY MyChamba.Infrastructure/*.csproj ./MyChamba.Infrastructure/

# Restaurar dependencias
RUN dotnet restore

# Copiar el código fuente de todos los proyectos
COPY MyChamba.API/. ./MyChamba.API/
COPY MyChamba.Application/. ./MyChamba.Application/
COPY MyChamba.Domain/. ./MyChamba.Domain/
COPY MyChamba.Infrastructure/. ./MyChamba.Infrastructure/

# Publicar en modo Release
WORKDIR /app/MyChamba.API
RUN dotnet publish -c Release -o /out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "MyChamba.API.dll"]