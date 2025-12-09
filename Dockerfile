# --- ETAPA 1: BUILD ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore "Proyectos-AGS/Proyectos-AGS.csproj"

WORKDIR "/src/Proyectos-AGS"
RUN dotnet publish "Proyectos-AGS.csproj" -c Release -o /app/publish

# --- ETAPA 2: RUNTIME ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Proyectos-AGS.dll"]