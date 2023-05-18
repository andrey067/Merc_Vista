FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app

COPY src/Domain/Domain.csproj /app/src/Domain/
COPY src/Application/Application.csproj /app/src/Application/
COPY src/Infrastructure/Infrastructure.csproj /app/src/Infrastructure/
COPY src/Presentation/Presentation.csproj /app/src/Presentation/
COPY src/Merc_Vista_Api/Merc_Vista_Api.csproj /app/src/Merc_Vista_Api/
COPY src/Tests/Merc_Vista.Tests/Merc_Vista.Tests.csproj /app/src/Tests/Merc_Vista.Tests/

RUN dotnet restore /app/src/Merc_Vista_Api/Merc_Vista_Api.csproj

COPY . ./
RUN dotnet publish -o /app/published-app

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
EXPOSE 80

COPY --from=build /app/published-app /app
CMD ASPNETCORE_URLS="http://*:$PORT" dotnet Merc_Vista_Api.dll