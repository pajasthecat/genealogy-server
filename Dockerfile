FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY genealogy-server.sln .
COPY src/ ./src/
RUN dotnet restore

COPY Geneology.Api/. ./Geneology.Api/
WORKDIR /src/Geneology.Api
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS runtime
WORKDIR /src
COPY --from=build /app/Geneology.Api/out ./
ENTRYPOINT ["dotnet", "Geneology.Api.dll"]
