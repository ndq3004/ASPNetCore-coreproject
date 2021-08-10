FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base

WORKDIR /app
COPY ["backend/config/", "config/"]

FROM mcr.microsoft.com/dotnet/sdk:3.1 as build
WORKDIR /src
COPY ["backend/src/NDQUAN.Auth.API/NDQUAN.Auth.API.csproj", "backend/src/NDQUAN.Auth.API/"]
COPY ["backend/core/NDQUAN.Core.DL/NDQUAN.Core.DL.csproj", "backend/core/NDQUAN.Core.DL/"]
COPY ["backend/core/NDQUAN.Core.Models/NDQUAN.Core.Models.csproj", "backend/core/NDQUAN.Core.Models/"]
COPY ["backend/core/NDQUAN.Core.Web/NDQUAN.Core.Web.csproj", "backend/core/NDQUAN.Core.Web/"]
COPY ["backend/startup/BaseStartup.csproj", "backend/startup/"]

RUN dotnet restore "backend/src/NDQUAN.Auth.API/NDQUAN.Auth.API.csproj"

COPY . .
