# Stage 1
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /build
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Stage 2
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "MovieTheatre.Api.dll"]
