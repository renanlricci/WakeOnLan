#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["WakeOnLan.Api/WakeOnLan.Api.csproj", "WakeOnLan.Api/"]
COPY ["WakeOnLan.Services/WakeOnLan.Services.csproj", "WakeOnLan.Services/"]
COPY ["WakeOnLan.Domain/WakeOnLan.Domain.csproj", "WakeOnLan.Domain/"]
COPY ["WakeOnLan.CrossCutting.Configuration/WakeOnLan.CrossCutting.Configuration.csproj", "WakeOnLan.CrossCutting.Configuration/"]
COPY ["WakeOnLan.CrossCutting.Exceptions/WakeOnLan.CrossCutting.Exceptions.csproj", "WakeOnLan.CrossCutting.Exceptions/"]
RUN dotnet restore "WakeOnLan.Api/WakeOnLan.Api.csproj"
COPY . .
WORKDIR "/src/WakeOnLan.Api"
RUN dotnet build "WakeOnLan.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WakeOnLan.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WakeOnLan.Api.dll"]