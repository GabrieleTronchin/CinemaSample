#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Cinema/Cinema.API/Cinema.Api.csproj", "Cinema/Cinema.API/"]
COPY ["Shared/Api.Common/Api.Common.csproj", "Shared/Api.Common/"]
COPY ["Cinema/Cinema.Application/Cinema.Application.csproj", "Cinema/Cinema.Application/"]
COPY ["Cinema/Cinema.Persistence/Cinema.Persistence.csproj", "Cinema/Cinema.Persistence/"]
COPY ["Cinema/Cinema.Domain/Cinema.Domain.csproj", "Cinema/Cinema.Domain/"]
RUN dotnet restore "Cinema/Cinema.API/Cinema.Api.csproj"
COPY . .
WORKDIR "/src/Cinema/Cinema.API"
RUN dotnet build "Cinema.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cinema.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cinema.Api.dll"]