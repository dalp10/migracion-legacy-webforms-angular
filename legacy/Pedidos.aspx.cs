// EJEMPLO ILUSTRATIVO del sistema legacy (el "antes"). No se ejecuta tal cual.
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace TuEmpresa.Legacy
{
    public partial class Pedidos : System.Web.UI.Page
    {
        // Todo vive en la página: acceso a datos, reglas de negocio y estado de la UI.
        private static string Cn => ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            int clienteId = int.Parse(txtCliente.Text);
            ViewState["ClienteId"] = clienteId;           // estado guardado en la página
            CargarPedidos(clienteId);
        }

        private void CargarPedidos(int clienteId)
        {
            using (var cn = new SqlConnection(Cn))
            using (var da = new SqlDataAdapter("SELECT Id, Total, Estado FROM Pedidos WHERE ClienteId = " + clienteId, cn))
            {
                var dt = new DataTable();
                da.Fill(dt);
                gvPedidos.DataSource = dt;
                gvPedidos.DataBind();
            }
        }

        protected void gvPedidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Aprobar") return;

            // La sesión guarda el usuario; sin ella, no hay seguridad real.
            if (Session["usuario"] == null) { Response.Redirect("Login.aspx"); return; }

            int pedidoId = Convert.ToInt32(gvPedidos.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
            int clienteId = (int)ViewState["ClienteId"];

            // Regla de negocio escondida en el evento del botón:
            // no se aprueba un pedido si el cliente tiene facturas vencidas.
            using (var cn = new SqlConnection(Cn))
            {
                cn.Open();
                var cmd = new SqlCommand("SELECT COUNT(*) FROM Facturas WHERE ClienteId = " + clienteId + " AND Vencida = 1", cn);
                if ((int)cmd.ExecuteScalar() > 0)
                {
                    lblMensaje.Text = "El cliente tiene facturas vencidas.";
                    return;
                }

                new SqlCommand("UPDATE Pedidos SET Estado = 'Aprobado' WHERE Id = " + pedidoId, cn).ExecuteNonQuery();
            }

            CargarPedidos(clienteId);
        }

        // Otra regla escondida: el descuento por antigüedad se calcula aquí.
        protected decimal CalcularDescuento(DateTime fechaRegistro)
        {
            int anios = DateTime.Now.Year - fechaRegistro.Year;
            if (anios >= 5) return 0.15m;
            if (anios >= 2) return 0.10m;
            return 0m;
        }
    }
}
