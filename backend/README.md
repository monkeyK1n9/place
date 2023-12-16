# Place - Online Event Reservation Application

## About Place

Discover unforgettable experiences with Place. From live DJ performances by world-renowned artists to cozy evenings in charming cocktail bars, explore a variety of nights out. Festivals and more...
Place - Uncover the Finest Local Events and Activities.

## Technologies

- **Backend**: ASP .NET Core 8
- **Database**: PostgreSQL
- **Architecture**: Clean Architecture
- **Containerization**: Docker

## Project Structure

The project is organized into several main directories:

- `src`: Contains the application's source code, divided into several projects:
  - `Place.Api.Application`: Business logic and application services.
  - `Place.Api.Domain`: Domain models and business rules.
  - `Place.Api.Infrastructure`: Database configuration, persistence, and external services.
  - `Place.Api.Presentation`: Controllers, endpoints, and Swagger configuration.
  - `Place.Api.Tests.Domain`: Unit tests for the domain models.
- `docs`: Project documentation, including modeling diagrams.
- `automation`: Scripts and configurations for automation.


## Getting Started

### With Docker

1. **Clone the repository**:

```bash
git clone https://github.com/osscameroon/place.git
```

2. **Navigate to the project directory**:

```bash
cd place/backend/Place.Api
```
3. **Build and start containers** using Docker Compose:

```bash
docker-compose up --build
```

### Without Docker

1. **Ensure PostgreSQL is running** with the configuration specified in `appsettings.json`.
2. **Navigate to the `src/Place.Api.Presentation` directory**.
3. **Clean the project** (optional)

```bash
dotnet clean
```

4. **Build the project**:

```bash
dotnet build
```

5. **Run the application**:

```bash
dotnet run
```


## Database Configuration

Configure PostgreSQL in `appsettings.json` under `Place.Api.Infrastructure`:

```json
{
"Postgres": {
 "ConnectionString": "Host=127.0.0.1;Port=5432;Database=placedb;Username=postgres;Password=password"
}
}
```

## Running Migrations
To apply migrations, execute:

```bash
dotnet ef database update --project src/Place.Api.Infrastructure/Place.Api.Infrastructure.csproj --startup-project src/Place.Api.Presentation/Place.Api.Presentation.csproj --context Place.Api.Infrastructure.Persistence.Authentication.EF.Contexts.UserReadDbContext --configuration Debug <migration_filename>
```

## Swagger
Access the Swagger UI at http://localhost:[port]/swagger once the application is running.



## Contributing
We use Conventional Commits for commit messages and Semantic Versioning for versioning.

For detailed guidelines, refer to CONTRIBUTING.md.

## License
This project is licensed under MIT Liencse. See LICENSE for more details.
