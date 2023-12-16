#!/bin/bash
echo ""

echo "Setting up environment variables for Docker Compose..."

ask() {
    local prompt default reply

    prompt="$1"
    default="$2"
    read -p "$prompt [$default]: " reply
    echo "${reply:-$default}"
}

echo ""

# Ask for the values of the environment variables
ASPNETCORE_ENVIRONMENT=$(ask "Enter ASP .NET Environment" "Development")
JWTSETTINGS_SECRET=$(ask "Enter JWT Secret" "OnceUponATimePlaceDb")
JWTSETTINGS_EXPIRYMINUTES=$(ask "Enter JWT Expiry Minutes" "300")
JWTSETTINGS_ISSUER=$(ask "Enter JWT Issuer" "PlaceApi")
JWTSETTINGS_AUDIENCE=$(ask "Enter JWT Audience" "PlaceApi")
SWAGGER_ENABLED=$(ask "Enable Swagger" "true")
SWAGGER_TITLE=$(ask "Swagger Title" "Place Api")
SWAGGER_VERSION=$(ask "Swagger Version" "v1")
SWAGGER_ROUTE=$(ask "Swagger Route" "swagger")
DOTNET_CONTAINER_NAME=$(ask "Dotnet Container Name" "oss-place-api-dotnet")
DOTNET_CONTAINER_PORT=$(ask "Dotnet Container Port" "5000")
POSTGRES_CONTAINER_NAME=$(ask "Postgres Container Name" "oss-postgres-database")
POSTGRES_DB=$(ask "Postgres DB Name" "ossplaceapidotnetdb")
POSTGRES_PORT=$(ask "Postgres Port" "5432")
POSTGRES_USER=$(ask "Postgres User" "postgres")
POSTGRES_PASSWORD=$(ask "Postgres Password" "password")
PGADMIN_CONTAINER_NAME=$(ask "PgAdmin Container Name" "oss-pgadmin")
PGADMIN_DEFAULT_EMAIL=$(ask "PgAdmin Default Email" "admin@admin.com")
PGADMIN_DEFAULT_PASSWORD=$(ask "PgAdmin Default Password" "admin")
PGADMIN_PORT=$(ask "PgAdmin Port" "8005")

echo ""



ask_for_hosts() {
    OSS_PLACE_API=$(ask "Enter the domain for oss-place-api.com" "oss-place-api.com")
    WWW_OSS_PLACE_API=$(ask "Enter the domain for www.oss-place-api.com" "www.oss-place-api.com")
    OSS_PGADMIN=$(ask "Enter the domain for oss-pgadmin.com" "oss-pgadmin.com")
    WWW_OSS_PGADMIN=$(ask "Enter the domain for www.oss-pgadmin.com" "www.oss-pgadmin.com")

    update_hosts $OSS_PLACE_API $WWW_OSS_PLACE_API $OSS_PGADMIN $WWW_OSS_PGADMIN
}


update_hosts() {
    local ip="127.0.0.1"
    local hosts=("$@")

    echo "Updating system hosts file, this may require sudo password..."
    for host in "${hosts[@]}"; do
        if ! grep -q "$host" /etc/hosts; then
            echo "Adding $host to /etc/hosts"
            echo "$ip $host" | sudo tee -a /etc/hosts > /dev/null
        else
            echo "$host already in /etc/hosts"
        fi
    done
}

# Create .env file
cat <<EOF > .env
ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT
JWTSETTINGS_SECRET=$JWTSETTINGS_SECRET
JWTSETTINGS_EXPIRYMINUTES=$JWTSETTINGS_EXPIRYMINUTES
JWTSETTINGS_ISSUER=$JWTSETTINGS_ISSUER
JWTSETTINGS_AUDIENCE=$JWTSETTINGS_AUDIENCE
SWAGGER_ENABLED=$SWAGGER_ENABLED
SWAGGER_TITLE=$SWAGGER_TITLE
SWAGGER_VERSION=$SWAGGER_VERSION
SWAGGER_ROUTE=$SWAGGER_ROUTE
DOTNET_CONTAINER_NAME=$DOTNET_CONTAINER_NAME
DOTNET_CONTAINER_PORT=$DOTNET_CONTAINER_PORT
POSTGRES_CONTAINER_NAME=$POSTGRES_CONTAINER_NAME
POSTGRES_DB=$POSTGRES_DB
POSTGRES_PORT=$POSTGRES_PORT
POSTGRES_USER=$POSTGRES_USER
POSTGRES_PASSWORD=$POSTGRES_PASSWORD
PGADMIN_CONTAINER_NAME=$PGADMIN_CONTAINER_NAME
PGADMIN_DEFAULT_EMAIL=$PGADMIN_DEFAULT_EMAIL
PGADMIN_DEFAULT_PASSWORD=$PGADMIN_DEFAULT_PASSWORD
PGADMIN_PORT=$PGADMIN_PORT
EOF

echo ""
echo "Setting up virtual host..."
echo ""

ask_for_hosts

echo ""
echo "Virtuals hosts set."

echo ""

echo "Environment variables set. Launching Docker Compose..."

echo ""
docker-compose up -d
echo ""