FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app
EXPOSE 80

WORKDIR /src
COPY *.csproj ./
COPY ./Database/Seeds/Scaffolds ./Scaffolds]

RUN dotnet restore LunchReporterAPI.csproj

# Copy everything else and build
COPY . .

RUN dotnet publish LunchReporterAPI.csproj  -c Release -o /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app .
ENTRYPOINT ["dotnet", "LunchReporterAPI.dll"]
