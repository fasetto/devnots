
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build
WORKDIR /app

COPY . .

RUN dotnet restore && \
    dotnet publish DevNots.sln -c Release -o build --no-restore && \
    cp ./DOKKU_SCALE /app/build

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as runtime

WORKDIR /app

COPY --from=build /app/build .

ENV ASPNETCORE_URLS="http://*:5000"
ENTRYPOINT ["dotnet", "DevNots.RestApi.dll"]
