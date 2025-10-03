using OrdersApi.Domain.Enums;

namespace OrdersApi.Application.DTOs
{
    // DTO para criação de ocorrência por cliente/API
    public class OcorrenciaCreateDto
    {
        public ETipoOcorrencia TipoOcorrencia { get; set; }
        public DateTime? HoraOcorrencia { get; set; }
    }
}
