using FluentValidation;
using OrdersApi.Application.DTOs;

namespace OrdersApi.Application.Validators
{
    // Validação simples para entrada de ocorrências
    public class OcorrenciaCreateDtoValidator : AbstractValidator<OcorrenciaCreateDto>
    {
        public OcorrenciaCreateDtoValidator()
        {
            RuleFor(x => x.TipoOcorrencia).IsInEnum().WithMessage("Tipo de ocorrência inválido");
            RuleFor(x => x.HoraOcorrencia).LessThanOrEqualTo(DateTime.UtcNow).When(x => x.HoraOcorrencia.HasValue).WithMessage("Hora da ocorrência não pode ser no futuro");
        }
    }
}
