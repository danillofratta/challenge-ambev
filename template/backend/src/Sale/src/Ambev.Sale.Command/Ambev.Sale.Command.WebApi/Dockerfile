# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Sale/src/Ambev.Sale.Command/Ambev.Sale.Command.WebApi/Ambev.Sale.Command.WebApi.csproj", "Sale/src/Ambev.Sale.Command/Ambev.Sale.Command.WebApi/"]
COPY ["Base/src/Ambev.Base.WebApi/Ambev.Base.WebApi.csproj", "Base/src/Ambev.Base.WebApi/"]
COPY ["Base/src/Ambev.Base.Domain/Ambev.Base.Domain.csproj", "Base/src/Ambev.Base.Domain/"]
COPY ["Sale/src/Ambev.Sale.Contracts/Ambev.Sale.Contracts.csproj", "Sale/src/Ambev.Sale.Contracts/"]
COPY ["Sale/src/Ambev.Sale.Command/Ambev.Sale.Command.Application/Ambev.Sale.Command.Application.csproj", "Sale/src/Ambev.Sale.Command/Ambev.Sale.Command.Application/"]
COPY ["Base/src/Ambev.Base.Infrastructure.Messaging.Rebus/Ambev.Base.Infrastructure.Messaging.Rebus.csproj", "Base/src/Ambev.Base.Infrastructure.Messaging.Rebus/"]
COPY ["Base/src/Ambev.Base.Infrastructure.Messaging/Ambev.Base.Infrastructure.Messaging.csproj", "Base/src/Ambev.Base.Infrastructure.Messaging/"]
COPY ["Sale/src/Ambev.Sale.Command/Ambev.Sale.Command.Domain/Ambev.Sale.Command.Domain.csproj", "Sale/src/Ambev.Sale.Command/Ambev.Sale.Command.Domain/"]
COPY ["Sale/src/Ambev.Sale.Command/Ambev.Sale.Command.Infrastructure.Orm/Ambev.Sale.Command.Infrastructure.Orm.csproj", "Sale/src/Ambev.Sale.Command/Ambev.Sale.Command.Infrastructure.Orm/"]
COPY ["Base/src/Ambev.Base.Infrastructure.Command.Orm/Ambev.Base.Infrastructure.Command.Orm.csproj", "Base/src/Ambev.Base.Infrastructure.Command.Orm/"]
COPY ["Base/src/Ambev.Base.Infrastructure.Query.Orm/Ambev.Base.Infrastructure.Query.Orm.csproj", "Base/src/Ambev.Base.Infrastructure.Query.Orm/"]
RUN dotnet restore "./Sale/src/Ambev.Sale.Command/Ambev.Sale.Command.WebApi/Ambev.Sale.Command.WebApi.csproj"
COPY . .
WORKDIR "/src/Sale/src/Ambev.Sale.Command/Ambev.Sale.Command.WebApi"
RUN dotnet build "./Ambev.Sale.Command.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ambev.Sale.Command.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ambev.Sale.Command.WebApi.dll"]