# Demander une valeur avec une valeur par défaut
Write-Host ""

function Ask($prompt, $default) {
    Write-Host -NoNewline "$prompt [$default]: "
    $input = Read-Host
    if ([string]::IsNullOrEmpty($input)) {
        return $default
    }
    return $input
}

Write-Host ""

# Ask for the values of the environment variables
$JWTSETTINGS_SECRET = Ask "Enter JWT Secret" "OnceUponATimePlaceDb"
$JWTSETTINGS_EXPIRYMINUTES = Ask "Enter JWT Expiry Minutes" "300"
$JWTSETTINGS_ISSUER = Ask "Enter JWT Issuer" "PlaceApi")
$JWTSETTINGS_AUDIENCE = Ask "Enter JWT Audience" "PlaceApi")
$SWAGGER_ENABLED = Ask "Enable Swagger" "true")
$SWAGGER_TITLE = Ask "Swagger Title" "Place Api")
$SWAGGER_VERSION = Ask "Swagger Version" "v1")
$SWAGGER_ROUTE = Ask "Swagger Route" "swagger")
$DOTNET_CONTAINER_NAME = Ask "Dotnet Container Name" "oss-place-api-dotnet")
$DOTNET_CONTAINER_PORT = Ask "Dotnet Container Port" "5000")
$POSTGRES_CONTAINER_NAME = Ask "Postgres Container Name" "oss-postgres-database")
$POSTGRES_DB = Ask "Postgres DB Name" "ossplaceapidotnetdb")
$POSTGRES_PORT = Ask "Postgres Port" "5432")
$POSTGRES_USER = Ask "Postgres User" "postgres")
$POSTGRES_PASSWORD = Ask "Postgres Password" "password")
$PGADMIN_CONTAINER_NAME = Ask "PgAdmin Container Name" "oss-pgadmin")
$PGADMIN_DEFAULT_EMAIL = Ask "PgAdmin Default Email" "admin@admin.com")
$PGADMIN_DEFAULT_PASSWORD = Ask "PgAdmin Default Password" "admin")
$PGADMIN_PORT = Ask "PgAdmin Port" "8005")

Write-Host ""

# Ask for domain addresses for the hosts file
Function Ask-For-Hosts {
    $OSS_PLACE_API = Ask "Enter the domain for oss-place-api.com" "oss-place-api.com"
    $WWW_OSS_PLACE_API = Ask "Enter the domain for www.oss-place-api.com" "www.oss-place-api.com"
    $OSS_PGADMIN = Ask "Enter the domain for oss-pgadmin.com" "oss-pgadmin.com"
    $WWW_OSS_PGADMIN = Ask "Enter the domain for www.oss-pgadmin.com" "www.oss-pgadmin.com"

    Update-Hosts -hosts @($OSS_PLACE_API, $WWW_OSS_PLACE_API, $OSS_PGADMIN, $WWW_OSS_PGADMIN)
}

Function Update-Hosts {
    param (
        [string]$ip = "127.0.0.1",
        [string[]]$hosts
    )

    $hostsPath = "$env:windir\System32\drivers\etc\hosts"
    $hostsFile = Get-Content $hostsPath

    foreach ($host in $hosts) {
        if ($hostsFile -notcontains "$ip $host") {
            Write-Host "Adding $host to hosts file"
            "$ip $host" | Add-Content $hostsPath
        } else {
            Write-Host "$host already in hosts file"
        }
    }
}


# Create .env file
@"
JWTSETTINGS_SECRET=$JWTSETTINGS_SECRET
JWTSETTINGS_EXPIRYMINUTES=$JWTSETTINGS_EXPIRYMINUTES
JWTSETTINGS_SECRET=$JWTSETTINGS_SECRET
JWTSETTINGS_EXPIRYMINUTES=$JWTSETTINGS_EXPIRYMINUTES
JWTSETTINGS_ISSUER=$JWTSETTINGS_ISSUER
JWTSETTINGS_AUDIENCE=$JWTSETTINGS_AUDIENCE

# Swagger Configuration
SWAGGER_ENABLED=$SWAGGER_ENABLED
SWAGGER_TITLE=$SWAGGER_TITLE
SWAGGER_VERSION=$SWAGGER_VERSION
SWAGGER_ROUTE=$SWAGGER_ROUTE

# Place api container config
DOTNET_CONTAINER_NAME=$DOTNET_CONTAINER_NAME
DOTNET_CONTAINER_PORT=$DOTNET_CONTAINER_PORT

# Postgres database configurations
POSTGRES_CONTAINER_NAME=$POSTGRES_CONTAINER_NAME
POSTGRES_DB=$POSTGRES_DB
POSTGRES_PORT=$POSTGRES_PORT
POSTGRES_USER=$POSTGRES_USER
POSTGRES_PASSWORD=$POSTGRES_PASSWORD

# PgAdmin configurations
PGADMIN_CONTAINER_NAME=$PGADMIN_CONTAINER_NAME
PGADMIN_DEFAULT_EMAIL=$PGADMIN_DEFAULT_EMAIL
PGADMIN_DEFAULT_PASSWORD=$PGADMIN_DEFAULT_PASSWORD
PGADMIN_PORT=$PGADMIN_PORT
# ... Autres variables ...
"@ | Out-File -FilePath .env



Write-Host "Setting up virtual host..."
Write-Host ""
Ask-For-Hosts

Write-Host ""
Write-Host "Virtuals hosts set."
Write-Host ""

# Lauch Docker Compose
Write-Host "Environment variables set. Launching Docker Compose..."

Write-Host ""
docker-compose up -d
Write-Host ""
