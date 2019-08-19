FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["FindThem/FindThem.csproj", "FindThem/"]
RUN dotnet restore "FindThem/FindThem.csproj"
COPY . .
WORKDIR "/src/FindThem"
RUN dotnet build "FindThem.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FindThem.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FindThem.dll"]