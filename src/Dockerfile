ARG FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
ARG buildenv=Docker
WORKDIR /src
COPY ["*.sln", "./"]

COPY ["WakeOnLan.Api/WakeOnLan.Api.csproj", "./WakeOnLan.Api/"]
COPY ["WakeOnLan.Services/WakeOnLan.Services.csproj", "./WakeOnLan.Services/"]
COPY ["WakeOnLan.Domain/WakeOnLan.Domain.csproj", "./WakeOnLan.Domain/"]
COPY ["WakeOnLan.CrossCutting.Configuration/WakeOnLan.CrossCutting.Configuration.csproj", "./WakeOnLan.CrossCutting.Configuration/"]
COPY ["WakeOnLan.CrossCutting.Exceptions/WakeOnLan.CrossCutting.Exceptions.csproj", "./WakeOnLan.CrossCutting.Exceptions/"]


RUN dotnet restore "./WakeOnLan.Api/WakeOnLan.Api.csproj"
COPY . .
WORKDIR /src/WakeOnLan.Api
RUN dotnet publish "WakeOnLan.Api/WakeOnLan.Api.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "WakeOnLan.Api.dll"]

EXPOSE 8080