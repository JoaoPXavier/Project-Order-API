using Microsoft.EntityFrameworkCore;
using OrdersApi.Domain.Entities;

namespace OrdersApi.Infrastructure.Data
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> opts) : base(opts) { }

        public DbSet<Pedido> Pedidos { get; set; } = null!;
        public DbSet<Ocorrencia> Ocorrencias { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>(eb =>
            {
                eb.HasKey(p => p.IdPedido);
                eb.Property(p => p.HoraPedido).IsRequired();
                eb.Property(p => p.IndEntregue).IsRequired();
                
                // Value Object Configuration
                eb.OwnsOne(p => p.NumeroPedido, vo =>
                {
                    vo.Property(v => v.Numero).HasColumnName("NumeroPedido");
                });

               
                eb.HasMany(p => p.Ocorrencias)
                  .WithOne(o => o.Pedido)
                  .HasForeignKey(o => o.PedidoId)
                  .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Ocorrencia>(eb =>
            {
                eb.HasKey(o => o.IdOcorrencia);
                eb.Property(o => o.TipoOcorrencia).IsRequired();
                eb.Property(o => o.HoraOcorrencia).IsRequired();
                eb.Property(o => o.IndFinalizadora).IsRequired();
                eb.Property(o => o.PedidoId).IsRequired();
            });
        }
    }
}