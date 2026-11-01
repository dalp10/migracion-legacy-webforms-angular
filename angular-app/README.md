# angular-app

Frontend del caso de estudio (módulo de Pedidos), listo para ejecutar. Angular 17, componentes *standalone*, RxJS e interceptores HTTP.

```bash
npm install
npm start          # http://localhost:4200
```

Las llamadas a `/api` se redirigen a `http://localhost:5000` mediante `proxy.conf.json`, así que primero levanta la API (ver README principal).

Los archivos del libro (capítulos 7–10) están en `src/app/`:

| Archivo | Capítulo |
|---|---|
| `core/interceptors/auth.interceptor.ts` | 7 y 10 |
| `pedidos/services/pedido.service.ts` | 8 y 10 |
| `pedidos/pedidos.routes.ts` | 8 |
| `pedidos/pedidos-list/` | 9 |

`app.component.*` es solo un *shell* mínimo (login de demostración y selector de cliente) para poder ejecutar el módulo de forma aislada.
