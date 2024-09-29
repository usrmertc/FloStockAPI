FROM postgres:16.4
ENV POSTGRES_PASSWORD=P@ssWord!23
ENV POSTGRES_DB=stocksdb
EXPOSE 5432

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FloAPI/FloAPI.csproj", "FloAPI/"]
COPY ["FloAPI.Business/FloAPI.Business.csproj", "FloAPI.Business/"]
COPY ["FloAPI.DataAccess/FloAPI.DataAccess.csproj", "FloAPI.DataAccess/"]
RUN dotnet restore "FloAPI/FloAPI.csproj"
COPY . .
WORKDIR "/src/FloAPI"
RUN dotnet build "FloAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FloAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FloAPI.dll"]