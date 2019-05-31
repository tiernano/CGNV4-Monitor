FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

COPY CGNV4-Monitor/*.csproj ./CGNV4-Monitor/
WORKDIR /app/CGNV4-Monitor
RUN dotnet restore

WORKDIR /app/
COPY CGNV4-Monitor/. ./CGNV4-Monitor/
WORKDIR /app/CGNV4-Monitor
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/runtime:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/CGNV4-Monitor/out ./
ENTRYPOINT ["dotnet", "CGNV4-Monitor.dll"]