#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Ambev.DeveloperEvaluation.IoC/Ambev.DeveloperEvaluation.IoC.csproj", "src/Ambev.DeveloperEvaluation.IoC/"]
COPY ["src/Ambev.DeveloperEvaluation.Common/Ambev.DeveloperEvaluation.Common.csproj", "src/Ambev.DeveloperEvaluation.Common/"]

COPY ["src/Ambev.DeveloperEvaluation.Domain/Ambev.DeveloperEvaluation.User.Domain.csproj", "src/Ambev.DeveloperEvaluation.User.Domain/"]
COPY ["src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.User.WebApi.csproj", "src/Ambev.DeveloperEvaluation.User.WebApi/"]
COPY ["src/Ambev.DeveloperEvaluation.Application/Ambev.DeveloperEvaluation.User.Application.csproj", "src/Ambev.DeveloperEvaluation.User.Application/"]
COPY ["src/Ambev.DeveloperEvaluation.ORM/Ambev.DeveloperEvaluation.User.ORM.csprojr", "src/Ambev.DeveloperEvaluation.User.ORM/"]
RUN dotnet restore "./src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.User.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Ambev.DeveloperEvaluation.User.WebApi"
RUN dotnet build "./Ambev.DeveloperEvaluation.User.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ambev.DeveloperEvaluation.User.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ambev.DeveloperEvaluation.User.WebApi.dll"]