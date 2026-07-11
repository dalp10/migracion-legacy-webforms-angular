namespace TuEmpresa.Modulo.Api.Models;

public class Pedido
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public decimal Total { get; set; }
    public string Estado { get; set; } = "Pendiente";
    public DateTime FechaCreacion { get; set; }
}

public class Cliente
{
    public int Id { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; }
}

public class Factura
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public bool Vencida { get; set; }
}

public class Usuario
{
    public int Id { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Rol { get; set; } = "Usuario";
}
