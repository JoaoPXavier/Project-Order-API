namespace OrdersApi.Domain.ValueObjects
{
    //Value Object to encapsulate order number rules.
    public sealed class NumeroPedidoVO
    {
        public int Numero { get; }
        public NumeroPedidoVO(int numero)
        {
            if (numero <= 0) throw new ArgumentException("Numero do pedido deve ser positivo", nameof(numero));
            Numero = numero;
        }

        public override string ToString() => Numero.ToString();
    }
}
