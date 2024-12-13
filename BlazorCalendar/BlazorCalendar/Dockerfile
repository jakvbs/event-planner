FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BlazorCalendar/BlazorCalendar/BlazorCalendar.csproj", "BlazorCalendar/BlazorCalendar/"]
COPY ["BlazorCalendar/BlazorCalendar.Client/BlazorCalendar.Client.csproj", "BlazorCalendar/BlazorCalendar.Client/"]
RUN dotnet restore "BlazorCalendar/BlazorCalendar/BlazorCalendar.csproj"
COPY . .
WORKDIR "/src/BlazorCalendar/BlazorCalendarBlazorCalendar/BlazorCalendarBlazorCalendar/BlazorCalendar"
RUN dotnet build "BlazorCalendar.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BlazorCalendar.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorCalendar.dll"]
