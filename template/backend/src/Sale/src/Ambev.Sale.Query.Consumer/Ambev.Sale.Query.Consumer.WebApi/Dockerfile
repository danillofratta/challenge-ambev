# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 6000
EXPOSE 8081


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Sale/src/Ambev.Sale.Query.Consumer/Ambev.Sale.Query.Consumer.WebApi/Ambev.Sale.Query.Consumer.WebApi.csproj", "Sale/src/Ambev.Sale.Query.Consumer/Ambev.Sale.Query.Consumer.WebApi/"]
COPY ["Base/src/Ambev.Base.WebApi/Ambev.Base.WebApi.csproj", "Base/src/Ambev.Base.WebApi/"]
COPY ["Base/src/Ambev.Base.Domain/Ambev.Base.Domain.csproj", "Base/src/Ambev.Base.Domain/"]
COPY ["Sale/src/Ambev.Sale.Contracts/Ambev.Sale.Contracts.csproj", "Sale/src/Ambev.Sale.Contracts/"]
COPY ["Sale/src/Ambev.Sale.Query.Consumer/Ambev.Sale.Query.Consumer.Infrastructure.Orm/Ambev.Sale.Query.Consumer.Infrastructure.Orm.csproj", "Sale/src/Ambev.Sale.Query.Consumer/Ambev.Sale.Query.Consumer.Infrastructure.Orm/"]
COPY ["Base/src/Ambev.Base.Infrastructure.Command.Orm/Ambev.Base.Infrastructure.Command.Orm.csproj", "Base/src/Ambev.Base.Infrastructure.Command.Orm/"]
COPY ["Base/src/Ambev.Base.Infrastructure.Query.Orm/Ambev.Base.Infrastructure.Query.Orm.csproj", "Base/src/Ambev.Base.Infrastructure.Query.Orm/"]
COPY ["Sale/src/Ambev.Sale.Query/Ambev.Sale.Query.Domain/Ambev.Sale.Query.Domain.csproj", "Sale/src/Ambev.Sale.Query/Ambev.Sale.Query.Domain/"]
COPY ["Sale/src/Ambev.Sale.Query/Ambev.Sale.Query.Infrastructure.Orm/Ambev.Sale.Query.Infrastructure.Orm.csproj", "Sale/src/Ambev.Sale.Query/Ambev.Sale.Query.Infrastructure.Orm/"]
COPY ["Sale/src/Ambev.Sale.Query.Consumer/Ambev.Sale.Query.Consumer.Domain/Ambev.Sale.Query.Consumer.Domain.csproj", "Sale/src/Ambev.Sale.Query.Consumer/Ambev.Sale.Query.Consumer.Domain/"]
RUN dotnet restore "./Sale/src/Ambev.Sale.Query.Consumer/Ambev.Sale.Query.Consumer.WebApi/Ambev.Sale.Query.Consumer.WebApi.csproj"
COPY . .
WORKDIR "/src/Sale/src/Ambev.Sale.Query.Consumer/Ambev.Sale.Query.Consumer.WebApi"
RUN dotnet build "./Ambev.Sale.Query.Consumer.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ambev.Sale.Query.Consumer.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ambev.Sale.Query.Consumer.WebApi.dll"]