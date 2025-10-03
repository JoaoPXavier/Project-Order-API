using OrdersApi.Domain.Enums;
using OrdersApi.Domain.ValueObjects;

namespace OrdersApi.Domain.Entities
{
    public class Pedido
    {
        public int IdPedido { get; private set; }
        public NumeroPedidoVO NumeroPedido { get; private set; } = null!;
        public DateTime HoraPedido { get; private set; }
        public bool IndEntregue { get; private set; }

        
        private List<Ocorrencia> _ocorrencias = new();
        public IReadOnlyCollection<Ocorrencia> Ocorrencias => _ocorrencias.AsReadOnly();

        private Pedido() { } // EF

        public Pedido(NumeroPedidoVO numeroVO)
        {
            NumeroPedido = numeroVO;
            HoraPedido = DateTime.UtcNow;
        }

        public bool IsConcluded => _ocorrencias.Any(o => o.IndFinalizadora);

        public Ocorrencia AddOcorrencia(ETipoOcorrencia tipo, DateTime hora)
        {
            if (IsConcluded)
                throw new InvalidOperationException("Pedido já está concluído. Não é possível adicionar ocorrência.");

            var lastSameType = _ocorrencias.Where(o => o.TipoOcorrencia == tipo)
                                           .OrderByDescending(o => o.HoraOcorrencia)
                                           .FirstOrDefault();
            if (lastSameType != null)
            {
                var delta = hora - lastSameType.HoraOcorrencia;
                if (delta.TotalMinutes < 10)
                    throw new InvalidOperationException("Não é permitido cadastrar duas ocorrências do mesmo tipo em menos de 10 minutos.");
            }

            bool isFinalizadora = _ocorrencias.Any();

            var ocorr = new Ocorrencia(tipo, hora, isFinalizadora, IdPedido);
            _ocorrencias.Add(ocorr);

            if (isFinalizadora)
                IndEntregue = tipo == ETipoOcorrencia.EntregueComSucesso;

            return ocorr;
        }

        public void RemoveOcorrencia(int idOcorrencia)
        {
            if (IsConcluded)
                throw new InvalidOperationException("Pedido concluído. Não é possível remover ocorrências.");

            var ocorr = _ocorrencias.FirstOrDefault(o => o.IdOcorrencia == idOcorrencia);
            if (ocorr == null)
                throw new KeyNotFoundException("Ocorrência não encontrada no pedido.");

            _ocorrencias.Remove(ocorr);
        }
    }
}