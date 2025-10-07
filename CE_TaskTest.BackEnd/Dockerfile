# ---------- build stage ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copia solo el csproj para cacheo (ajusta nombre del csproj)
COPY ["CE_TaskTest.BackEnd.csproj", "./"]
RUN dotnet restore "./CE_TaskTest.BackEnd.csproj"

# copia todo y publica
COPY . .
RUN dotnet publish "CE_TaskTest.BackEnd.csproj" -c Release -o /app/out

# ---------- runtime stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=build /app/out .

# Usamos la variable PORT que Render provee; fallback 8080
ENTRYPOINT ["/bin/sh", "-c", "ASPNETCORE_URLS=http://0.0.0.0:${PORT:-8080} dotnet CE_TaskTest.BackEnd.dll"]
