namespace wedding_system_api.Configuration
{
    public class MercadoPagoSettings    
    {
        public string AccessToken { get; set; } = string.Empty;
        public string WebhookSecret { get; set; } = string.Empty;
        public string? NotificationUrl { get; set; }
    }
}