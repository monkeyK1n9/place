FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Place.Api.Presentation/Place.Api.Presentation.csproj", "src/Place.Api.Presentation/"]
RUN dotnet restore "src/Place.Api.Presentation/Place.Api.Presentation.csproj"
COPY . .
WORKDIR "/src/src/Place.Api.Presentation"
RUN dotnet build "Place.Api.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Place.Api.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Place.Api.Presentation.dll"]
