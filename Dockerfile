# syntax=docker/dockerfile:1.7
ARG DOTNET_VERSION=8.0

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/PatientService.Api/PatientService.Api.csproj"
RUN dotnet publish "src/PatientService.Api/PatientService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app
COPY --from=build /app/publish ./
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "PatientService.Api.dll"]
