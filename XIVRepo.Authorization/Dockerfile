FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["XIVRepo.Authorization/XIVRepo.Authorization.csproj", "XIVRepo.Authorization/"]
RUN dotnet restore "XIVRepo.Authorization/XIVRepo.Authorization.csproj"
COPY . .
WORKDIR "/src/XIVRepo.Authorization"
RUN dotnet build "XIVRepo.Authorization.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "XIVRepo.Authorization.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XIVRepo.Authorization.dll"]
