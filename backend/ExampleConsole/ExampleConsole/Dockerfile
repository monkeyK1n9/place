FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ExampleConsole/ExampleConsole.csproj", "ExampleConsole/"]
RUN dotnet restore "ExampleConsole/ExampleConsole.csproj"
COPY . .
WORKDIR "/src/ExampleConsole"
RUN dotnet build "ExampleConsole.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExampleConsole.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExampleConsole.dll"]
