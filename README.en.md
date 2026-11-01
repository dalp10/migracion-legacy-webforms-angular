# Legacy Migration: WebForms to Angular and Modern .NET

[![CI](https://github.com/dalp10/migracion-legacy-webforms-angular/actions/workflows/ci.yml/badge.svg)](https://github.com/dalp10/migracion-legacy-webforms-angular/actions/workflows/ci.yml)
![.NET 8](https://img.shields.io/badge/.NET-8-512BD4)
![Angular 17](https://img.shields.io/badge/Angular-17-DD0031)
![MIT License](https://img.shields.io/badge/license-MIT-green)

🇪🇸 [Versión en español](README.md)

Reference code for the book **"Migración Legacy: WebForms a Angular y .NET Moderno sin Detener tu Negocio"** (written in Spanish).

It contains the full case study (an **Orders** module) used throughout the book, following the *strangler fig* pattern: the new system replaces the WebForms application screen by screen while both run side by side and the business keeps working.

| Before | After |
|---|---|
| [`legacy/`](legacy) — a WebForms page mixing SQL, business rules and `ViewState` in the code-behind | [`api/`](api) — .NET 8 Web API · [`angular-app/`](angular-app) — Angular 17 |

See [`docs/arquitectura.md`](docs/arquitectura.md) for the migration strategy and diagrams (in Spanish).

## What it demonstrates

- Exposing legacy business rules as a **.NET 8 Web API** (`PedidoService`, `PedidosController`).
- Moving the data layer to **Entity Framework Core** against the same SQL Server database.
- **JWT authentication** and an **Angular 17** frontend (standalone components, RxJS state, HTTP interceptors).
- **Unit tests with xUnit** that protect the business rules (order approval, customer discounts) before switching off the old page.
- **CI** with GitHub Actions (build and tests for both projects).

## Run it

Requirements: Docker and Node.js 20+.

```bash
cp .env.example .env            # change the passwords
docker compose up --build       # SQL Server + demo data + API
```

API and Swagger: <http://localhost:5000/swagger>. Then, in another terminal:

```bash
cd angular-app
npm install
npm start                       # http://localhost:4200
```

Demo data: customer `1` (6 years old) can have orders approved; customer `3` has an overdue invoice, so orders **cannot** be approved.

Tests: `cd api/TuEmpresa.Modulo.Api.Tests && dotnet test`.

## Before using it in production

This is an **educational template**, not production-ready code. In particular: the login endpoint is a placeholder that accepts any credentials, `GET /api/pedidos` is not authenticated, and the JWT key must come from environment variables or a secret store. See the Spanish README for the full list.

## Need to migrate your own WebForms system?

I do audits, maintenance and phased migrations of ASP.NET WebForms systems to .NET 8 + Angular, without stopping your operation.

- 🔗 [My Upwork profile](https://www.upwork.com/freelancers/~0153855e74cbb82a3f)
- 🔗 [GitHub: dalp10](https://github.com/dalp10)

## License

Example code for educational purposes, released under the [MIT license](LICENSE).
