using Microsoft.EntityFrameworkCore;
using TuEmpresa.Modulo.Api.Data;
using TuEmpresa.Modulo.Api.Models;
using TuEmpresa.Modulo.Api.Services;
using Xunit;

namespace TuEmpresa.Modulo.Api.Tests;

public class PedidoServiceTests
{
    private static ModuloDbContext CrearContextoEnMemoria()
    {
        var options = new DbContextOptionsBuilder<ModuloDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new ModuloDbContext(options);
    }

    [Fact]
    public async Task NoPermiteAprobarPedidoConFacturasVencidas()
    {
        var context = CrearContextoEnMemoria();
        context.Clientes.Add(new Cliente { Id = 1, FechaRegistro = DateTime.Now });
        context.Facturas.Add(new Factura { ClienteId = 1, Vencida = true });
        context.Pedidos.Add(new Pedido { Id = 1, ClienteId = 1 });
        await context.SaveChangesAsync();

        var service = new PedidoService(context);

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.AprobarPedido(1));
    }

    [Fact]
    public async Task PermiteAprobarPedidoSinFacturasVencidas()
    {
        var context = CrearContextoEnMemoria();
        context.Clientes.Add(new Cliente { Id = 1, FechaRegistro = DateTime.Now });
        context.Pedidos.Add(new Pedido { Id = 1, ClienteId = 1, Estado = "Pendiente" });
        await context.SaveChangesAsync();

        var service = new PedidoService(context);
        var resultado = await service.AprobarPedido(1);

        Assert.Equal("Aprobado", resultado.Estado);
    }

    [Theory]
    [InlineData(6, 0.15)]
    [InlineData(3, 0.10)]
    [InlineData(1, 0.0)]
    public void CalculaDescuentoSegunAntiguedad(int aniosAntiguedad, decimal descuentoEsperado)
    {
        var context = CrearContextoEnMemoria();
        var service = new PedidoService(context);
        var cliente = new Cliente { FechaRegistro = DateTime.Now.AddYears(-aniosAntiguedad) };

        var descuento = service.CalcularDescuentoCliente(cliente);

        Assert.Equal(descuentoEsperado, descuento);
    }
}
