namespace wedding_system_api.Settings
{
    public class MercadoPagoSettings
    {
        // Nome da seção na configuração. Corresponde ao prefixo "MercadoPago:"
        // usado no comando dotnet user-secrets set.
        public const string SectionName = "MercadoPago";

        // Token privado do Mercado Pago. Vem do User Secrets (desenvolvimento)
        // ou de variável de ambiente (produção). Nunca fica no código.
        public string AccessToken { get; set; } = string.Empty;
    }
}   