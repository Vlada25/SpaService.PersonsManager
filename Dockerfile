#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PersonsManager.API/PersonsManager.API.csproj", "PersonsManager.API/"]
COPY ["PersonsManager.Database/PersonsManager.Database.csproj", "PersonsManager.Database/"]
COPY ["PersonsManager.Domain/PersonsManager.Domain.csproj", "PersonsManager.Domain/"]
COPY ["PersonsManager.DTO/PersonsManager.DTO.csproj", "PersonsManager.DTO/"]
COPY ["PersonsManager.Interfaces/PersonsManager.Interfaces.csproj", "PersonsManager.Interfaces/"]
COPY ["PersonsManager.Messaging/PersonsManager.Messaging.csproj", "PersonsManager.Messaging/"]
RUN dotnet restore "PersonsManager.API/PersonsManager.API.csproj"
COPY . .
WORKDIR "/src/PersonsManager.API"
RUN dotnet build "PersonsManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PersonsManager.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonsManager.API.dll"]