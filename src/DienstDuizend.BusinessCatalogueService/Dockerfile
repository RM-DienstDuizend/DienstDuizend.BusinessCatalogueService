FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/DienstDuizend.BusinessCatalogueService/DienstDuizend.BusinessCatalogueService.csproj", "src/DienstDuizend.BusinessCatalogueService/"]
RUN dotnet restore "src/DienstDuizend.BusinessCatalogueService/DienstDuizend.BusinessCatalogueService.csproj"
COPY . .
WORKDIR "/src/src/DienstDuizend.BusinessCatalogueService"
RUN dotnet build "DienstDuizend.BusinessCatalogueService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DienstDuizend.BusinessCatalogueService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DienstDuizend.BusinessCatalogueService.dll"]
