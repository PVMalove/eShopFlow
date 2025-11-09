FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Promorion/Promotion.GRPS/Promotion.GRPS.csproj", "src/Services/Promorion/Promotion.GRPS/"]
COPY ["src/Shared/Common.Kernel/Common.Kernel.csproj", "src/Shared/Common.Kernel/"]
RUN dotnet restore "src/Services/Promorion/Promotion.GRPS/Promotion.GRPS.csproj"
COPY . .
WORKDIR "/src/src/Services/Promorion/Promotion.GRPS"
RUN dotnet build "./Promotion.GRPS.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Promotion.GRPS.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Promotion.GRPS.dll"]
