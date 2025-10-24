using Microsoft.AspNetCore.Mvc;
using OrdersApi.Application.DTOs;
using OrdersApi.Domain.ValueObjects;
using OrdersApi.Infrastructure.Repositories;

namespace OrdersApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepo;
        public PedidosController(IPedidoRepository pedidoRepo) => _pedidoRepo = pedidoRepo;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pedidos = await _pedidoRepo.ListAllAsync();
            return Ok(pedidos);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PedidoDto dto)
        {
            // var existe = await _pedidoRepo.ExisteNumeroPedidoAsync(dto.NumeroPedido);
            // if (existe) 
            // return BadRequest("There is already an order with this number");

            var numeroVo = new NumeroPedidoVO(dto.NumeroPedido);
            var pedido = new Domain.Entities.Pedido(numeroVo);
            await _pedidoRepo.AddAsync(pedido);
            await _pedidoRepo.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = pedido.IdPedido }, pedido);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pedido = await _pedidoRepo.GetWithOcorrenciasAsync(id);
            if (pedido == null) return NotFound();
            return Ok(pedido);
        }
    }
}