using Microsoft.EntityFrameworkCore;
using TuEmpresa.Modulo.Api.Models;

namespace TuEmpresa.Modulo.Api.Data;

public class ModuloDbContext : DbContext
{
    public ModuloDbContext(DbContextOptions<ModuloDbContext> options) : base(options) { }

    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Factura> Facturas => Set<Factura>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
}
