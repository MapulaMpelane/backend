# Use the official .NET 9.0 SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the source code
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out

# Use the official ASP.NET 9.0 runtime image for the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

# Expose ports (adjust if needed)
EXPOSE 5157
EXPOSE 7262

# Set environment variables for ASP.NET Core
ENV ASPNETCORE_URLS=http://+:5157;https://+:7262
ENV ASPNETCORE_ENVIRONMENT=Development

# Run the application
ENTRYPOINT ["dotnet", "my-application.dll"]