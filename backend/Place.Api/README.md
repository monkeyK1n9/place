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

1. **For windows Fsers**:

Run the `launch_project.ps1`` script using PowerShell:

```powershell
.\launch_project.ps1
```
This script will ask you for the necessary environment variables and update the system's `hosts` file. Please run PowerShell as an administrator to ensure the script can modify the `hosts` file.

2. **For Unix-like Users**:

Run the launch_project.sh script using Bash:

```bash
./launch_project.sh
```

This script will perform similar actions as the Windows script. You might need to enter your password to allow modifications to the `hosts` file.


3. **Docker compose**

After setting up the environment variables, Docker Compose will start the Place project automatically. The containers for the necessary services, including the database, API, and front-end, will be set up and connected.

4. **Accessing the Application**

Once the setup is complete, you can access the Place project in your browser:

- For the API, visit: `http://oss-place-api.com`
- For the PgAdmin, visit: `http://www.oss-pgadmin.com`

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
