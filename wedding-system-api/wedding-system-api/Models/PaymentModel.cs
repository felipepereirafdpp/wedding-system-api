namespace wedding_system_api.Models
{
    public enum PaymentStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2,
        Cancelled = 3,
        Refunded = 4
    }

    public class PaymentModel
    {
        public Guid Id { get; set; }

        // Presente que está sendo pago (liga com presentsModel.Id)
        public Guid PresentId { get; set; }

        // Valor cobrado, copiado do preço do presente NO MOMENTO da compra
        public decimal Amount { get; set; }

        // Status interno do nosso sistema
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        // Status original devolvido pelo Mercado Pago (ex.: "approved", "in_process")
        public string? MercadoPagoStatus { get; set; }

        // ID da preferência ou da order criada no Mercado Pago (definimos qual na Etapa 3)
        public string? MercadoPagoReferenceId { get; set; }

        // ID do pagamento no Mercado Pago, preenchido quando a notificação chegar
        public string? MercadoPagoPaymentId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}