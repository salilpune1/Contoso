#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Contoso.Api/Contoso.Api.csproj", "Contoso.Api/"]
COPY ["Contoso.Entity/Contoso.Entity.csproj", "Contoso.Entity/"]
COPY ["Contoso.Domain/Contoso.Domain.csproj", "Contoso.Domain/"]
RUN dotnet restore "Contoso.Api/Contoso.Api.csproj"
COPY . .
WORKDIR "/src/Contoso.Api"
RUN dotnet build "Contoso.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Contoso.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contoso.Api.dll"]