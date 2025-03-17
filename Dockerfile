FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
ENV HUSKY=0
WORKDIR /src

COPY ["Workshops.Web/Workshops.Web.csproj", "Workshops.Web.csproj/"]
COPY ["Workshops.Application/Workshops.Application.csproj", "Workshops.Application/"]
COPY ["Workshops.Infrastructure/Workshops.Infrastructure.csproj", "Workshops.Infrastructure/"]
COPY ["Workshops.Domain/Workshops.Domain.csproj", "Workshops.Domain/"]

RUN dotnet restore "Workshops.Web.csproj"
COPY . .
WORKDIR "/src/Workshops.Web"
RUN dotnet build "Workshops.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Workshops.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Workshops.Web.dll"]
