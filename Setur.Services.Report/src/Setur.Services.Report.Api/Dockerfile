#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Setur.Services.Report/src/Setur.Services.Report.Api/Setur.Services.Report.Api.csproj", "Setur.Services.Report/src/Setur.Services.Report.Api/"]
COPY ["Setur.Services.Report/src/Setur.Services.Report.Infrastructure/Setur.Services.Report.Infrastructure.csproj", "Setur.Services.Report/src/Setur.Services.Report.Infrastructure/"]
COPY ["Setur.Services.Report/src/Setur.Services.Report.Application/Setur.Services.Report.Application.csproj", "Setur.Services.Report/src/Setur.Services.Report.Application/"]
COPY ["Setur.Services.Report/src/Setur.Services.Report.Core/Setur.Services.Report.Core.csproj", "Setur.Services.Report/src/Setur.Services.Report.Core/"]
RUN dotnet restore "Setur.Services.Report/src/Setur.Services.Report.Api/Setur.Services.Report.Api.csproj"
COPY . .
WORKDIR "/src/Setur.Services.Report/src/Setur.Services.Report.Api"
RUN dotnet build "Setur.Services.Report.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Setur.Services.Report.Api.csproj" -c Release -o /app/publish

ENV TZ=Europe/Istanbul
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Setur.Services.Report.Api.dll"]