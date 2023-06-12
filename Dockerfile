#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WeatherForecast/WeatherForecast.Api.csproj", "WeatherForecast/"]
COPY ["WeatherForecast.Infrastructure/WeatherForecast.Infrastructure.csproj", "WeatherForecast.Infrastructure/"]
COPY ["WeatherForecast.Application/WeatherForecast.Application.csproj", "WeatherForecast.Application/"]
COPY ["WeatherForecast.Domain/WeatherForecast.Domain.csproj", "WeatherForecast.Domain/"]
RUN dotnet restore "WeatherForecast/WeatherForecast.Api.csproj"
COPY . .
WORKDIR "/src/WeatherForecast"
RUN dotnet build "WeatherForecast.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WeatherForecast.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherForecast.Api.dll"]