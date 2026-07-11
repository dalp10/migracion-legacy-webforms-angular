using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuEmpresa.Modulo.Api.Services;

namespace TuEmpresa.Modulo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly PedidoService _pedidoService;

    public PedidosController(PedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerPorCliente([FromQuery] int clienteId)
    {
        var pedidos = await _pedidoService.ObtenerPorCliente(clienteId);
        return Ok(pedidos);
    }

    [Authorize]
    [HttpPost("{id}/aprobar")]
    public async Task<IActionResult> Aprobar(int id)
    {
        try
        {
            var pedido = await _pedidoService.AprobarPedido(id);
            return Ok(pedido);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}
