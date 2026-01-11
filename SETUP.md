# Configuración del Proyecto

Este proyecto utiliza **User Secrets** de .NET para manejar configuraciones sensibles de forma segura.

## Configuración Inicial

### 1. Clonar el repositorio
```bash
git clone <tu-repositorio>
cd Firebase
```

### 2. Configurar User Secrets

El proyecto ya está inicializado con User Secrets. Solo necesitas agregar tus credenciales:

```bash
cd src/Api

# Configurar cadena de conexión SQLite
dotnet user-secrets set "ConnectionStrings:SqliteDatabase" "Data Source=YourDatabase.db"

# Configurar cadena de conexión PostgreSQL
dotnet user-secrets set "ConnectionStrings:ConnectionString" "Host=your-host;Port=5432;Database=your-db;Username=your-user;Password=your-password;"

# Configurar Firebase Authentication
dotnet user-secrets set "Authentication:TokenUri" "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=YOUR_FIREBASE_API_KEY"
dotnet user-secrets set "Authentication:Audience" "your-firebase-project-id"
dotnet user-secrets set "Authentication:ValidIssuer" "https://securetoken.google.com/your-firebase-project-id"
```

### 3. Verificar configuración

Para listar todos tus User Secrets:
```bash
dotnet user-secrets list
```

### 4. Ejecutar el proyecto

```bash
dotnet run
```

## Archivos de Configuración

- **appsettings.json**: Configuración base (SIN datos sensibles)
- **appsettings.Development.json**: Configuración específica para desarrollo (SIN datos sensibles)
- **appsettings.Example.json**: Plantilla de ejemplo con todas las configuraciones necesarias
- **User Secrets**: Almacenamiento seguro de credenciales (local, nunca en Git)

## Ubicación de User Secrets

Los User Secrets se almacenan de forma segura en:
- **Windows**: `%APPDATA%\Microsoft\UserSecrets\<user_secrets_id>\secrets.json`
- **macOS/Linux**: `~/.microsoft/usersecrets/<user_secrets_id>/secrets.json`

Estos archivos NUNCA se suben a Git.

## Variables de Entorno Necesarias

### Base de Datos
- `ConnectionStrings:SqliteDatabase`: Ruta a tu base de datos SQLite
- `ConnectionStrings:ConnectionString`: Cadena de conexión a PostgreSQL (Neon)

### Firebase Authentication
- `Authentication:TokenUri`: URL del endpoint de autenticación con tu API Key
- `Authentication:Audience`: ID de tu proyecto Firebase
- `Authentication:ValidIssuer`: URL del emisor de tokens de Firebase

## Despliegue en Producción

Para producción, configura estas variables como **variables de entorno** en tu servidor o plataforma de hosting:

```bash
# Ejemplo para Azure App Service, AWS, etc.
ConnectionStrings__SqliteDatabase="Data Source=production.db"
ConnectionStrings__ConnectionString="Host=prod-host;..."
Authentication__TokenUri="https://..."
Authentication__Audience="your-project-id"
Authentication__ValidIssuer="https://..."
```

Nota: En variables de entorno, usa `__` (doble guión bajo) en lugar de `:` para separadores jerárquicos.

## Seguridad

- ✅ User Secrets para desarrollo local
- ✅ Variables de entorno para producción
- ✅ `.gitignore` configurado correctamente
- ❌ NUNCA subas credenciales a Git
- ❌ NUNCA compartas tu `secrets.json`
