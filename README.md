# Migración Legacy: WebForms a Angular y .NET Moderno

[![CI](https://github.com/dalp10/migracion-legacy-webforms-angular/actions/workflows/ci.yml/badge.svg)](https://github.com/dalp10/migracion-legacy-webforms-angular/actions/workflows/ci.yml)
![.NET 8](https://img.shields.io/badge/.NET-8-512BD4)
![Angular 17](https://img.shields.io/badge/Angular-17-DD0031)
![Licencia MIT](https://img.shields.io/badge/licencia-MIT-green)

🇬🇧 [English version](README.en.md)

Repositorio de referencia del libro **«Migración Legacy: WebForms a Angular y .NET Moderno sin Detener tu Negocio»**.

Contiene el caso de estudio completo (módulo de **Pedidos**) que sirve de hilo conductor del libro, siguiendo el patrón *strangler fig*: el sistema nuevo reemplaza al WebForms pantalla por pantalla, mientras ambos conviven y el negocio sigue funcionando.

| Antes | Después |
|---|---|
| [`legacy/`](legacy) — pantalla WebForms con SQL, reglas de negocio y `ViewState` mezclados en el *code-behind* | [`api/`](api) — Web API en .NET 8 · [`angular-app/`](angular-app) — Angular 17 |

La estrategia completa, con diagramas, está en [`docs/arquitectura.md`](docs/arquitectura.md).

## Estructura

```
├── legacy/                            # El "antes": pantalla WebForms ilustrativa
├── api/
│   ├── TuEmpresa.Modulo.Api/          # Web API en .NET 8 (Capítulos 5-7)
│   │   ├── Controllers/               # PedidosController, AuthController
│   │   ├── Services/                  # PedidoService (lógica de negocio)
│   │   ├── Models/                    # Entidades
│   │   ├── Data/                      # DbContext (EF Core)
│   │   └── Program.cs                 # Configuración, JWT, CORS
│   └── TuEmpresa.Modulo.Api.Tests/    # Pruebas unitarias con xUnit (Capítulo 12)
├── angular-app/                       # Angular 17 completo y ejecutable (Capítulos 7-10)
├── docker/init.sql                    # Esquema y datos de demostración
├── docker-compose.yml                 # SQL Server + API en un comando
└── docs/                              # Arquitectura y estrategia de migración
```

## Mapeo capítulo → código

| Capítulo | Archivos relacionados |
|---|---|
| 5 — Exponer lógica de negocio como servicios | `PedidoService.cs`, `PedidosController.cs`, `Program.cs` |
| 6 — Migrar capa de datos | `ModuloDbContext.cs`, `Pedido.cs` |
| 7 — Autenticación JWT | `AuthController.cs`, `auth.interceptor.ts` |
| 8 — Arquitectura Angular | `pedido.service.ts`, `pedidos.routes.ts` |
| 9 — Reemplazar controles WebForms | `pedidos-list.component.ts/html` |
| 10 — Manejo de estado y RxJS | `pedido.service.ts` (`BehaviorSubject`), `auth.interceptor.ts` (`errorInterceptor`) |
| 12 — Testing | `PedidoServiceTests.cs` |

## Cómo ejecutarlo

### Opción A — con Docker (recomendada)

Requisitos: Docker y Node.js 20+.

```bash
cp .env.example .env            # cambia las contraseñas
docker compose up --build       # SQL Server + datos de demo + API
```

- API y Swagger: <http://localhost:5000/swagger>

En otra terminal:

```bash
cd angular-app
npm install
npm start                       # http://localhost:4200
```

Datos de demostración: el cliente `1` (6 años de antigüedad) puede aprobar pedidos; el cliente `3` tiene una factura vencida, así que **no** puede.

> En Mac con chip Apple Silicon, la imagen de SQL Server funciona bajo emulación y puede tardar más en arrancar.

### Opción B — sin Docker

```bash
cd api/TuEmpresa.Modulo.Api
dotnet restore
dotnet run
```

Configura tu cadena de conexión real en `appsettings.json` (o con `dotnet user-secrets`). El esquema que espera la API está en [`docker/init.sql`](docker/init.sql). Si ejecutas la API fuera de Docker, ajusta el destino del proxy en `angular-app/proxy.conf.json`.

### Pruebas

```bash
cd api/TuEmpresa.Modulo.Api.Tests
dotnet test
```

## Antes de llevarlo a producción

Este repositorio es una **plantilla didáctica**, no un proyecto listo para producción. Cada archivo corresponde a un ejemplo explicado en un capítulo del libro. Puntos que debes resolver en tu propio sistema:

- **`AuthController.Login` acepta cualquier usuario y contraseña.** Es un marcador de posición: reemplázalo por la validación real contra tu tabla de usuarios, reutilizando el esquema de hash del sistema legacy.
- **`GET /api/pedidos` no exige autenticación.** Cualquiera que conozca un `clienteId` podría listar sus pedidos. Protégelo con `[Authorize]` y valida que el usuario tenga acceso a ese cliente.
- **La clave JWT.** No subas tu `Jwt:Key` real a un repositorio público: usa variables de entorno o un almacén de secretos (ver Capítulo 7). La API debería negarse a arrancar sin clave en producción en lugar de usar un valor por defecto.
- **Fechas en UTC.** Usa `DateTime.UtcNow` para expiraciones de token y fechas de negocio.
- **Pedido inexistente.** Hoy `AprobarPedido` responde "facturas vencidas" aunque el pedido no exista; conviene devolver `404`.

## ¿Necesitas migrar tu propio sistema WebForms?

Hago auditorías, mantenimiento y migraciones de sistemas ASP.NET WebForms a .NET 8 + Angular, por fases y sin detener la operación.

- 🔗 [Mi perfil en Upwork](https://www.upwork.com/freelancers/~0153855e74cbb82a3f)
- 🔗 [GitHub: dalp10](https://github.com/dalp10)

## Licencia

Código de ejemplo con fines educativos, para acompañar el libro. Úsalo y adáptalo libremente bajo los términos de la licencia [MIT](LICENSE).
