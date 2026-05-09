FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["VeterinaryClinic.Bff.Api/VeterinaryClinic.Bff.Api.csproj", "VeterinaryClinic.Bff.Api/"]
COPY ["VeterinaryClinic.Bff.Domain/VeterinaryClinic.Bff.Domain.csproj", "VeterinaryClinic.Bff.Domain/"]
COPY ["VeterinaryClinic.Bff.Application/VeterinaryClinic.Bff.Application.csproj", "VeterinaryClinic.Bff.Application/"]
COPY ["VeterinaryClinic.Bff.Infrastructure/VeterinaryClinic.Bff.Infrastructure.csproj", "VeterinaryClinic.Bff.Infrastructure/"]

RUN dotnet restore "VeterinaryClinic.Bff.Api/VeterinaryClinic.Bff.Api.csproj"

COPY . .
WORKDIR /src/VeterinaryClinic.Bff.Api

RUN dotnet build "VeterinaryClinic.Bff.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VeterinaryClinic.Bff.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VeterinaryClinic.Bff.Api.dll"]