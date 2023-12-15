FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine as build

WORKDIR /src

COPY ["src/Place.Api.Application/Place.Api.Application.csproj", "src/Place.Api.Application/"]
COPY ["src/Place.Api.Domain/Place.Api.Domain.csproj", "src/Place.Api.Domain/"]
COPY ["src/Place.Api.Infrastructure/Place.Api.Infrastructure.csproj", "src/Place.Api.Infrastructure/"]
COPY ["src/Place.Api.Presentation/Place.Api.Presentation.csproj", "src/Place.Api.Presentation/"]

RUN dotnet clean "src/Place.Api.Application/Place.Api.Application.csproj"
RUN dotnet clean "src/Place.Api.Domain/Place.Api.Domain.csproj"
RUN dotnet clean "src/Place.Api.Infrastructure/Place.Api.Infrastructure.csproj"
RUN dotnet clean "src/Place.Api.Presentation/Place.Api.Presentation.csproj"

RUN dotnet restore "src/Place.Api.Presentation/Place.Api.Presentation.csproj" -r linux-musl-x64 /p:PublishReadyToRun=true

COPY . .

RUN dotnet publish "src/Place.Api.Presentation/Place.Api.Presentation.csproj" -c Release -r linux-musl-x64 -o /app/publish /p:PublishTrimmed=false  /p:PublishReadyToRun=true /p:PublishSingleFile=true --self-contained true

FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-alpine as final

EXPOSE  5000

ENV ASPNETCORE_URLS=http://+:5000;

RUN  apk add curl vim

RUN adduser --disabled-password --home /app --gecos '' nonroot && chown -R nonroot /app

USER nonroot

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT [ "./Place.Api.Presentation" ]