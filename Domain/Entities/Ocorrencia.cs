using OrdersApi.Domain.Enums;

namespace OrdersApi.Domain.Entities
{
    public class Ocorrencia
    {
        public int IdOcorrencia { get; private set; }
        public ETipoOcorrencia TipoOcorrencia { get; private set; }
        public DateTime HoraOcorrencia { get; private set; }
        public bool IndFinalizadora { get; private set; }

        public int PedidoId { get; private set; }
        public Pedido Pedido { get; private set; } = null!;

        private Ocorrencia() { } // EF Core

        
        public Ocorrencia(ETipoOcorrencia tipo, DateTime hora, bool indFinalizadora = false, int pedidoId = 0)
        {
            TipoOcorrencia = tipo;
            HoraOcorrencia = hora;
            IndFinalizadora = indFinalizadora;
            PedidoId = pedidoId;
        }

        public void MarkAsFinalizadora() => IndFinalizadora = true;
    }
}