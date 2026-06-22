FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Chaika.sln .
COPY Directory.Build.props .

COPY src/Chaika.Domain/Chaika.Domain.csproj src/Chaika.Domain/
COPY src/Chaika.Application/Chaika.Application.csproj src/Chaika.Application/
COPY src/Chaika.Infrastructure/Chaika.Infrastructure.csproj src/Chaika.Infrastructure/
COPY src/Chaika.Api/Chaika.Api.csproj src/Chaika.Api/
COPY shared/Chaika.Contracts/Chaika.Contracts.csproj shared/Chaika.Contracts/
COPY shared/Chaika.Client/Chaika.Client.csproj shared/Chaika.Client/
COPY tests/Chaika.Tests/Chaika.Tests.csproj tests/Chaika.Tests/

RUN dotnet restore

COPY . .

RUN dotnet publish src/Chaika.Api -c Release -o /app/publish --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

RUN useradd -m -u 1000 appuser && chown -R appuser /app
USER appuser

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Chaika.Api.dll"]
