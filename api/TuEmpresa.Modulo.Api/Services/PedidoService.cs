using Microsoft.EntityFrameworkCore;
using TuEmpresa.Modulo.Api.Data;
using TuEmpresa.Modulo.Api.Models;

namespace TuEmpresa.Modulo.Api.Services;

public class PedidoService
{
    private readonly ModuloDbContext _context;

    public PedidoService(ModuloDbContext context)
    {
        _context = context;
    }

    public async Task<List<Pedido>> ObtenerPorCliente(int clienteId)
    {
        return await _context.Pedidos
            .Where(p => p.ClienteId == clienteId)
            .ToListAsync();
    }

    public async Task<bool> PuedeAprobarsePedido(int pedidoId)
    {
        var pedido = await _context.Pedidos.FindAsync(pedidoId);
        if (pedido is null) return false;

        var facturasVencidas = await _context.Facturas
            .Where(f => f.ClienteId == pedido.ClienteId && f.Vencida)
            .AnyAsync();

        return !facturasVencidas;
    }

    public async Task<Pedido> AprobarPedido(int pedidoId)
    {
        if (!await PuedeAprobarsePedido(pedidoId))
            throw new InvalidOperationException("El cliente tiene facturas vencidas.");

        var pedido = await _context.Pedidos.FindAsync(pedidoId);
        pedido!.Estado = "Aprobado";
        await _context.SaveChangesAsync();
        return pedido;
    }

    public decimal CalcularDescuentoCliente(Cliente cliente)
    {
        var antiguedadAnios = DateTime.Now.Year - cliente.FechaRegistro.Year;

        if (antiguedadAnios >= 5) return 0.15m;
        if (antiguedadAnios >= 2) return 0.10m;
        return 0m;
    }
}
