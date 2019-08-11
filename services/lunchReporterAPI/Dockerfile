FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app
EXPOSE 80

WORKDIR /src
COPY ["services/lunchReporterAPI/LunchReporterAPI.csproj", "services/lunchReporterAPI/"]
COPY ["services/dataModels/DataModels.csproj", "services/dataModels/"]
RUN dotnet restore "services/lunchReporterAPI/LunchReporterAPI.csproj"

# Copy everything else and build
COPY . .
WORKDIR /src/services/lunchReporterAPI
RUN dotnet publish LunchReporterAPI.csproj  -c Release -o /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app .
ENTRYPOINT ["dotnet", "LunchReporterAPI.dll"]
