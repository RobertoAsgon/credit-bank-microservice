FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY Register.API.csproj .
RUN dotnet restore "Register.API.csproj"
COPY . .
RUN dotnet build "Register.API.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Register.API.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Register.API.dll"]