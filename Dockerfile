FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5176

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["BlazorCalendar.sln", "."]
COPY ["BlazorCalendar/BlazorCalendar/BlazorCalendar.csproj", "BlazorCalendar/BlazorCalendar/"]
COPY ["BlazorCalendar/BlazorCalendar.Client/BlazorCalendar.Client.csproj", "BlazorCalendar/BlazorCalendar.Client/"]
COPY ["BlazorCalendar.Application/BlazorCalendar.Application.csproj", "BlazorCalendar.Application/"]
COPY ["BlazorCalendar.Domain/BlazorCalendar.Domain.csproj", "BlazorCalendar.Domain/"]
COPY ["BlazorCalendar.Infrastructure/BlazorCalendar.Infrastructure.csproj", "BlazorCalendar.Infrastructure/"]
COPY ["BlazorCalendar.Shared/BlazorCalendar.Shared.csproj", "BlazorCalendar.Shared/"]

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR "/src/BlazorCalendar/BlazorCalendar"
RUN dotnet build "BlazorCalendar.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BlazorCalendar.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorCalendar.dll"]
