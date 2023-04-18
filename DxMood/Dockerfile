# Start with a .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Set the working directory
WORKDIR /app

# Copy the .csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the source code
COPY . ./

# Build the app
RUN dotnet publish -c Release -o out

# Start with a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Set the working directory
WORKDIR /app

# Copy the published app
COPY --from=build-env /app/out .

# Expose port 80
EXPOSE 80

# Start the app
ENTRYPOINT ["dotnet", "DxMood.dll"]
