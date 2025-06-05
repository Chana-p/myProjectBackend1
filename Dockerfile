# Use the official .NET 8 runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["CPC#PROJECT/Server.csproj", "CPC#PROJECT/"]
COPY ["BL/BL.csproj", "BL/"]
COPY ["Dal/Dal.csproj", "Dal/"]

RUN dotnet restore "CPC#PROJECT/Server.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/CPC#PROJECT"
RUN dotnet build "Server.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Server.dll"]
