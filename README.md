# Migración Legacy: WebForms a Angular y .NET Moderno

Repositorio de referencia del libro **"Migración Legacy: WebForms a Angular y .NET Moderno sin Detener tu Negocio"**.

Contiene el código del caso de estudio completo (módulo de Pedidos) usado como hilo conductor a lo largo del libro, siguiendo el patrón *strangler fig*.

## Estructura del repositorio

```
├── api/
│   ├── TuEmpresa.Modulo.Api/          # Web API en .NET 8 (Capítulos 5-7)
│   │   ├── Controllers/               # PedidosController, AuthController
│   │   ├── Services/                  # PedidoService (lógica de negocio)
│   │   ├── Models/                    # Entidades
│   │   ├── Data/                      # DbContext (EF Core)
│   │   └── Program.cs                 # Configuración, JWT, CORS
│   └── TuEmpresa.Modulo.Api.Tests/    # Pruebas unitarias (Capítulo 12)
├── angular-app/
│   └── src/app/
│       ├── core/interceptors/         # Auth + manejo de errores (Cap. 7, 10)
│       ├── pedidos/                   # Módulo de pedidos (Cap. 8-10)
│       └── shared/models/             # Modelos TypeScript
└── docs/
```

## Cómo usar este código

Este repositorio es una **plantilla de referencia**, no un proyecto listo para producción. Cada archivo corresponde a un ejemplo explicado paso a paso en un capítulo específico del libro — revisa el capítulo correspondiente antes de adaptar el código a tu propio sistema.

### Backend (.NET 8)

```bash
cd api/TuEmpresa.Modulo.Api
dotnet restore
dotnet run
```

Configura tu cadena de conexión real en `appsettings.json` antes de ejecutar. **No subas tu `Jwt:Key` real a un repositorio público** — usa variables de entorno o un almacén de secretos (ver Capítulo 7).

### Tests

```bash
cd api/TuEmpresa.Modulo.Api.Tests
dotnet test
```

### Frontend (Angular)

Este repositorio incluye solo los archivos de código fuente relevantes mencionados en el libro (no un proyecto Angular completo con `package.json`/`angular.json`). Para usarlo:

```bash
ng new angular-app --standalone --routing
# luego copia los archivos de src/app/ de este repositorio dentro de tu proyecto nuevo
```

## Mapeo capítulo → código

| Capítulo | Archivos relacionados |
|---|---|
| 5 — Exponer lógica de negocio como servicios | `PedidoService.cs`, `PedidosController.cs`, `Program.cs` |
| 6 — Migrar capa de datos | `ModuloDbContext.cs`, `Pedido.cs` |
| 7 — Autenticación JWT | `AuthController.cs`, `auth.interceptor.ts` |
| 8 — Arquitectura Angular | `pedido.service.ts`, `pedidos.routes.ts` |
| 9 — Reemplazar controles WebForms | `pedidos-list.component.ts/html` |
| 10 — Manejo de estado y RxJS | `pedido.service.ts` (BehaviorSubject), `auth.interceptor.ts` (errorInterceptor) |
| 12 — Testing | `PedidoServiceTests.cs` |

## Licencia

Código de ejemplo proporcionado con fines educativos, para acompañar el libro. Úsalo y adáptalo libremente en tus propios proyectos bajo los términos de la [licencia MIT](LICENSE).
