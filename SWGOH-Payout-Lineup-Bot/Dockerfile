FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY SWGOH-Payout-Lineup-Bot/SWGOH-Payout-Lineup-Bot.csproj SWGOH-Payout-Lineup-Bot/
RUN dotnet restore SWGOH-Payout-Lineup-Bot/SWGOH-Payout-Lineup-Bot.csproj
COPY . .
WORKDIR /src/SWGOH-Payout-Lineup-Bot
RUN dotnet build SWGOH-Payout-Lineup-Bot.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SWGOH-Payout-Lineup-Bot.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SWGOH-Payout-Lineup-Bot.dll"]
