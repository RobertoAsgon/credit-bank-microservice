FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY Gateway.API.csproj .
RUN dotnet restore "Gateway.API.csproj"
COPY . .
RUN dotnet build "Gateway.API.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Gateway.API.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateway.API.dll"]