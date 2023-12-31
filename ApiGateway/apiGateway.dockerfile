# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# Copy the project file and restore as distinct layers
COPY ApiGateway.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . ./

# Build the application in Release mode
RUN dotnet publish -c Release -o out

# Stage 2: Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App

# Copy the published application from the build-env stage
COPY --from=build-env /App/out .

# Set the entry point
ENTRYPOINT ["dotnet", "ApiGateway.dll"]
