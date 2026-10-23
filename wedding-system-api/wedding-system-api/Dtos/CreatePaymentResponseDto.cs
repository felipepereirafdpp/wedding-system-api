namespace wedding_system_api.Dtos
{
    public class CreatePaymentResponseDto
    {
        // Id do registro de pagamento no NOSSO banco
        public Guid PaymentId { get; set; }

        // Id da preferência criada no Mercado Pago
        public string PreferenceId { get; set; } = string.Empty;

        // URL para onde o React redireciona o convidado (Checkout Pro)
        public string CheckoutUrl { get; set; } = string.Empty;
    }
}