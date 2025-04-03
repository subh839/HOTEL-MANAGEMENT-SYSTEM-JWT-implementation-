# Use the official .NET 6 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app



# Copy the remaining files and build the application
COPY . .
WORKDIR "/app/JWTAuth"
RUN dotnet publish -c Release -o /publish

# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /publish .

# Expose port 5000 for HTTP and 5001 for HTTPS
EXPOSE 5000
EXPOSE 5001

# Set the entry point
ENTRYPOINT ["dotnet", "JWTAuth.dll"]
