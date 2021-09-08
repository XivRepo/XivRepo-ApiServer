FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["XIVRepo.GraphQL/XIVRepo.GraphQL.csproj", "XIVRepo.GraphQL/"]
RUN dotnet restore "XIVRepo.GraphQL/XIVRepo.GraphQL.csproj"
COPY . .
WORKDIR "/src/XIVRepo.GraphQL"
RUN dotnet build "XIVRepo.GraphQL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "XIVRepo.GraphQL.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XIVRepo.GraphQL.dll"]
