
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
<<<<<<<< HEAD:template/backend/src/User/Dockerfile
COPY ["src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj", "src/Ambev.DeveloperEvaluation.WebApi/"]
COPY ["src/Ambev.DeveloperEvaluation.Application/Ambev.DeveloperEvaluation.Application.csproj", "src/Ambev.DeveloperEvaluation.Application/"]
COPY ["src/Ambev.DeveloperEvaluation.Common/Ambev.DeveloperEvaluation.Common.csproj", "src/Ambev.DeveloperEvaluation.Common/"]
COPY ["src/Ambev.DeveloperEvaluation.Domain/Ambev.DeveloperEvaluation.Domain.csproj", "src/Ambev.DeveloperEvaluation.Domain/"]
COPY ["src/Ambev.DeveloperEvaluation.IoC/Ambev.DeveloperEvaluation.IoC.csproj", "src/Ambev.DeveloperEvaluation.IoC/"]
COPY ["src/Ambev.DeveloperEvaluation.ORM/Ambev.DeveloperEvaluation.ORM.csproj", "src/Ambev.DeveloperEvaluation.ORM/"]
RUN dotnet restore "./src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj"
========

COPY ["src/Ambev.DeveloperEvaluation.IoC/Ambev.DeveloperEvaluation.IoC.csproj", "src/Ambev.DeveloperEvaluation.IoC/"]
COPY ["src/Ambev.DeveloperEvaluation.Common/Ambev.DeveloperEvaluation.Common.csproj", "src/Ambev.DeveloperEvaluation.Common/"]

COPY ["src/Ambev.DeveloperEvaluation.Domain/Ambev.DeveloperEvaluation.User.Domain.csproj", "src/Ambev.DeveloperEvaluation.User.Domain/"]
COPY ["src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.User.WebApi.csproj", "src/Ambev.DeveloperEvaluation.User.WebApi/"]
COPY ["src/Ambev.DeveloperEvaluation.Application/Ambev.DeveloperEvaluation.User.Application.csproj", "src/Ambev.DeveloperEvaluation.User.Application/"]
COPY ["src/Ambev.DeveloperEvaluation.ORM/Ambev.DeveloperEvaluation.User.ORM.csprojr", "src/Ambev.DeveloperEvaluation.User.ORM/"]
RUN dotnet restore "./src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.User.WebApi.csproj"
>>>>>>>> b0:template/backend/src/User/src/Ambev.DeveloperEvaluation.User.WebApi/Dockerfile
COPY . .
WORKDIR "/src/src/Ambev.DeveloperEvaluation.User.WebApi"
RUN dotnet build "./Ambev.DeveloperEvaluation.User.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ambev.DeveloperEvaluation.User.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
<<<<<<<< HEAD:template/backend/src/User/Dockerfile
ENTRYPOINT ["dotnet", "Ambev.DeveloperEvaluation.WebApi.dll"]
========
ENTRYPOINT ["dotnet", "Ambev.DeveloperEvaluation.User.WebApi.dll"]
>>>>>>>> b0:template/backend/src/User/src/Ambev.DeveloperEvaluation.User.WebApi/Dockerfile
