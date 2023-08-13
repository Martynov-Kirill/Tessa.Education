#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TASS.Education.API/Tessa.Education.API.csproj", "TASS.Education.API/"]
COPY ["TASS.Education.BLL/Tessa.Education.BLL.csproj", "TASS.Education.BLL/"]
COPY ["TASS.Education.DAL/Tessa.Education.DAL.csproj", "TASS.Education.DAL/"]
COPY ["TASS.Education.Entites/Tessa.Education.Entites.csproj", "TASS.Education.Entites/"]
RUN dotnet restore "TASS.Education.API/Tessa.Education.API.csproj"
COPY . .
WORKDIR "/src/TASS.Education.API"
RUN dotnet build "Tessa.Education.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tessa.Education.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tessa.Education.API.dll"]