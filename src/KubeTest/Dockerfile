FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src

COPY src/KubeTest/KubeTest.csproj ./src/KubeTest/

RUN dotnet restore src/KubeTest/KubeTest.csproj
COPY . .
WORKDIR /src/src/KubeTest
RUN dotnet publish -c Release -o /app
FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "KubeTest.dll"]