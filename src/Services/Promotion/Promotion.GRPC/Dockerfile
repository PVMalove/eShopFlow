FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Promotion/Promotion.GRPC/Promotion.GRPC.csproj", "src/Services/Promotion/Promotion.GRPC/"]
COPY ["src/Shared/Common.Kernel/Common.Kernel.csproj", "src/Shared/Common.Kernel/"]
RUN dotnet restore "src/Services/Promotion/Promotion.GRPC/Promotion.GRPC.csproj"
COPY . .
WORKDIR "/src/src/Services/Promotion/Promotion.GRPC"
RUN dotnet build "./Promotion.GRPC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Promotion.GRPC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Promotion.GRPC.dll"]
