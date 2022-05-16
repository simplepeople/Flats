﻿FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Runner/Runner.csproj", "Runner/"]
RUN dotnet restore "Runner/Runner.csproj"
COPY . .
WORKDIR "/src/Runner"
RUN dotnet build "Runner.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Runner.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Runner.dll"]
