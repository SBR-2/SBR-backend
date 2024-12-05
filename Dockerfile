FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SBRSystem-API/SBRSystem-API.csproj", "SBRSystem-API/"]
COPY ["SBRSystem-Data/SBRSystem-Data.csproj", "SBRSystem-Data/"]
COPY ["SBRSystem-Repository/SBRSystem-Repository.csproj", "SBRSystem-Repository/"]
RUN dotnet restore "SBRSystem-API/SBRSystem-API.csproj"
COPY . .
WORKDIR "/src/SBRSystem-API"
RUN dotnet build "SBRSystem-API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SBRSystem-API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SBRSystem-API.dll"]
