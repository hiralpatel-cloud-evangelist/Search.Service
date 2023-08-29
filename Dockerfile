#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Search.Service/Search.Service.csproj", "Search.Service/"]
COPY ["SearchService.Base/SearchService.Base.csproj", "SearchService.Base/"]
COPY ["SearchService.DTO/SearchService.DTO.csproj", "SearchService.DTO/"]
COPY ["SearchService.Models/SearchService.Models.csproj", "SearchService.Models/"]
COPY ["SearchService.Services/SearchService.Services.csproj", "SearchService.Services/"]
RUN dotnet restore "Search.Service/Search.Service.csproj"
COPY . .
WORKDIR "/src/Search.Service"
RUN dotnet build "Search.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Search.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Search.Service.dll"]