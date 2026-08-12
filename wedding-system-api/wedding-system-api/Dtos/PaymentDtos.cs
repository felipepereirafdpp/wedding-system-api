namespace wedding_system_api.Dtos
{
    // Retornado ao criar a preferência — o frontend usa InitPoint ( link que redireciona para a tela da api do mercado pago) pra redirecionar
    public class PreferenceResponseDto
    {
        public string InitPoint { get; set; } = string.Empty;
        public string PreferenceId { get; set; } = string.Empty;
    }

    // Retornado quando o frontend consulta o status (ex: ao voltar do checkout)
    public class PaymentStatusResponseDto
    {
        public Guid PresentId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? PaymentId { get; set; }
    }
}