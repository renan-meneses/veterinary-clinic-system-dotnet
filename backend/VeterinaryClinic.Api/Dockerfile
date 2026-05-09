FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["VeterinaryClinic.Api/VeterinaryClinic.Api.csproj", "VeterinaryClinic.Api/"]
COPY ["VeterinaryClinic.Domain/VeterinaryClinic.Domain.csproj", "VeterinaryClinic.Domain/"]
COPY ["VeterinaryClinic.Application/VeterinaryClinic.Application.csproj", "VeterinaryClinic.Application/"]
COPY ["VeterinaryClinic.Infrastructure/VeterinaryClinic.Infrastructure.csproj", "VeterinaryClinic.Infrastructure/"]
COPY ["VeterinaryClinic.Infrastructure.Queries/VeterinaryClinic.Infrastructure.Queries.csproj", "VeterinaryClinic.Infrastructure.Queries/"]
COPY ["VeterinaryClinic.Infrastructure.Cache/VeterinaryClinic.Infrastructure.Cache.csproj", "VeterinaryClinic.Infrastructure.Cache/"]

RUN dotnet restore "VeterinaryClinic.Api/VeterinaryClinic.Api.csproj"

COPY . .
WORKDIR /src/VeterinaryClinic.Api

RUN dotnet build "VeterinaryClinic.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VeterinaryClinic.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VeterinaryClinic.Api.dll"]