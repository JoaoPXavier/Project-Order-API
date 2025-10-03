using Microsoft.AspNetCore.Mvc;
using OrdersApi.Application.DTOs;
using OrdersApi.Infrastructure.Repositories;

namespace OrdersApi.Api.Controllers
{
    [ApiController]
    [Route("api/pedidos/{pedidoId}/[controller]")]
    public class OcorrenciasController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepo;
        private readonly IOcorrenciaRepository _ocorrRepo;
        public OcorrenciasController(IPedidoRepository pedidoRepo, IOcorrenciaRepository ocorrRepo)
        {
            _pedidoRepo = pedidoRepo;
            _ocorrRepo = ocorrRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int pedidoId, [FromBody] OcorrenciaCreateDto dto)
        {
            var pedido = await _pedidoRepo.GetWithOcorrenciasAsync(pedidoId);
            if (pedido == null) return NotFound("Pedido não encontrado");

            var hora = dto.HoraOcorrencia ?? DateTime.UtcNow;

            
            var ocorr = pedido.AddOcorrencia(dto.TipoOcorrencia, hora);

            await _ocorrRepo.AddAsync(ocorr);
            _pedidoRepo.Update(pedido);
            await _pedidoRepo.SaveChangesAsync();

            return CreatedAtAction(null, new { id = ocorr.IdOcorrencia }, ocorr);
        }

        [HttpDelete("{ocorrenciaId}")]
        public async Task<IActionResult> Delete(int pedidoId, int ocorrenciaId)
        {
            var pedido = await _pedidoRepo.GetWithOcorrenciasAsync(pedidoId);
            if (pedido == null) return NotFound("Pedido não encontrado");

            if (pedido.IsConcluded)
                return BadRequest("Pedido concluído. Não é possível excluir ocorrências.");

            pedido.RemoveOcorrencia(ocorrenciaId);
            _pedidoRepo.Update(pedido);
            await _pedidoRepo.SaveChangesAsync();
            return NoContent();
        }
    }
}