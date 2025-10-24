using OrdersApi.Domain.Enums;

namespace OrdersApi.Application.DTOs
{
    // DTO for creating an occurrence by client/API
    public class OcorrenciaCreateDto
    {
        public ETipoOcorrencia TipoOcorrencia { get; set; }
        public DateTime? HoraOcorrencia { get; set; }
    }
}
