#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["Setur.Services.Contact/src/Setur.Services.Contact.Api/Setur.Services.Contact.Api.csproj", "Setur.Services.Contact/src/Setur.Services.Contact.Api/"]
COPY ["Setur.Services.Contact/src/Setur.Services.Contact.Infrastructure/Setur.Services.Contact.Infrastructure.csproj", "Setur.Services.Contact/src/Setur.Services.Contact.Infrastructure/"]
COPY ["Setur.Services.Contact/src/Setur.Services.Contact.Application/Setur.Services.Contact.Application.csproj", "Setur.Services.Contact/src/Setur.Services.Contact.Application/"]
COPY ["Setur.Services.Contact/src/Setur.Services.Contact.Core/Setur.Services.Contact.Core.csproj", "Setur.Services.Contact/src/Setur.Services.Contact.Core/"]
RUN dotnet restore "Setur.Services.Contact/src/Setur.Services.Contact.Api/Setur.Services.Contact.Api.csproj"
COPY . .
WORKDIR "/src/Setur.Services.Contact/src/Setur.Services.Contact.Api"
RUN dotnet build "Setur.Services.Contact.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Setur.Services.Contact.Api.csproj" -c Release -o /app/publish

ENV TZ=Europe/Istanbul
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Setur.Services.Contact.Api.dll"]