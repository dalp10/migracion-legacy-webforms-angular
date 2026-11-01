# legacy/ — el "antes"

Ejemplo **ilustrativo** de cómo suele verse una pantalla de pedidos en ASP.NET WebForms antes de migrar. Sirve para comparar, lado a lado, con el resultado en `api/` y `angular-app/`.

Está escrito para mostrar los patrones que el libro enseña a desmontar; **no está pensado para ejecutarse** ni incluye el proyecto completo.

| En WebForms (`Pedidos.aspx` / `.aspx.cs`) | Dónde termina tras la migración |
|---|---|
| Consulta SQL dentro del *code-behind* | `PedidoService.ObtenerPorCliente` (Capítulos 5 y 6) |
| Regla "no aprobar con facturas vencidas" dentro del evento del botón | `PedidoService.PuedeAprobarsePedido` / `AprobarPedido` (Capítulo 5) |
| Descuento por antigüedad calculado en la página | `PedidoService.CalcularDescuentoCliente` (Capítulo 5) |
| `GridView` + `UpdatePanel` + `ViewState` | `pedidos-list.component` + `BehaviorSubject` (Capítulos 9 y 10) |
| Sesión de ASP.NET (`Session["usuario"]`) | JWT + `auth.interceptor.ts` (Capítulo 7) |
