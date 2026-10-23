using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wedding_system_api.Models;

namespace wedding_system_api.Data
{
    public class wedding_system_apiContext : DbContext
    {
        public wedding_system_apiContext(DbContextOptions<wedding_system_apiContext> options)
            : base(options)
        {
        }

        public DbSet<wedding_system_api.Models.presentsModel> presentsModel { get; set; } = default!;

        // NOVO: tabela de pagamentos
        public DbSet<PaymentModel> Payments { get; set; } = default!;

        // NOVO: regras de mapeamento da tabela de pagamentos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PaymentModel>(entity =>
            {
                // Dinheiro: até 18 dígitos, 2 casas decimais
                entity.Property(p => p.Amount).HasPrecision(18, 2);

                // Guarda o status como texto legivel ("Approved") em vez de numero
                entity.Property(p => p.Status).HasConversion<string>().HasMaxLength(20);

                // Cada pagamento pertence a um presente.
                // Restrict = impede apagar um presente que ja tenha pagamentos.
                entity.HasOne<presentsModel>()
                      .WithMany()
                      .HasForeignKey(p => p.PresentId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Um mesmo pagamento do Mercado Pago nao pode ser registrado duas vezes
                entity.HasIndex(p => p.MercadoPagoPaymentId).IsUnique();
            });
        }
    }
}