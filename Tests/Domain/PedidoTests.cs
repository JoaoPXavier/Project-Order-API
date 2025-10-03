using OrdersApi.Domain.Entities;
using OrdersApi.Domain.Enums;
using OrdersApi.Domain.ValueObjects;
using Xunit;

namespace OrdersApi.Test.Domain
{
    public class PedidoTests
    {
        [Fact]
        public void AddOcorrencia_Should_MarkSecondAsFinalizadora_AndSetIndEntregueTrue_WhenEntregueComSucesso()
        {
            // Arrange
            var pedido = new Pedido(new NumeroPedidoVO(1));

            // Act
            pedido.AddOcorrencia(ETipoOcorrencia.EmRotaDeEntrega, DateTime.UtcNow);
            var second = pedido.AddOcorrencia(ETipoOcorrencia.EntregueComSucesso, DateTime.UtcNow.AddMinutes(11));

            // Assert
            Assert.True(second.IndFinalizadora);
            Assert.True(pedido.IndEntregue);
            Assert.True(pedido.IsConcluded);
        }

        [Fact]
        public void AddOcorrencia_SameTypeWithin10Min_ShouldThrow()
        {
            // Arrange
            var pedido = new Pedido(new NumeroPedidoVO(2));
            pedido.AddOcorrencia(ETipoOcorrencia.EmRotaDeEntrega, DateTime.UtcNow);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                pedido.AddOcorrencia(ETipoOcorrencia.EmRotaDeEntrega, DateTime.UtcNow.AddMinutes(5))
            );
        }

        [Fact]
        public void RemoveOcorrencia_OnConcluded_ShouldThrow()
        {
            // Arrange
            var pedido = new Pedido(new NumeroPedidoVO(3));
            pedido.AddOcorrencia(ETipoOcorrencia.EmRotaDeEntrega, DateTime.UtcNow);
            pedido.AddOcorrencia(ETipoOcorrencia.AvariaNoProduto, DateTime.UtcNow.AddMinutes(11));

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => pedido.RemoveOcorrencia(1));
        }
    }
}
