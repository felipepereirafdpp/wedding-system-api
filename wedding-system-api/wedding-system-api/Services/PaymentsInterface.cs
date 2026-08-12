using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using MercadoPago.Client.Payment;
using MercadoPago.Client.Preference;
using MercadoPago.Resource.Payment;
using wedding_system_api.Configuration;
using wedding_system_api.Dtos;
using wedding_system_api.InterfaceServices;
using wedding_system_api.Models;

namespace wedding_system_api.Services
{
    public class PaymentService : IPaymentInterface
    {
        private readonly IPresentInterface _presentService;
        private readonly MercadoPagoSettings _mpSettings;
        private readonly FrontendSettings _frontendSettings;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(
            IPresentInterface presentService,
            IOptions<MercadoPagoSettings> mpSettings,
            IOptions<FrontendSettings> frontendSettings,
            ILogger<PaymentService> logger)
        {
            _presentService = presentService;
            _mpSettings = mpSettings.Value;
            _frontendSettings = frontendSettings.Value;
            _logger = logger;
        }

        public async Task<PreferenceResponseDto> CriarPreferenciaAsync(Guid presentId)
        {
            var present = await _presentService.GetByIdAsync(presentId)
                ?? throw new KeyNotFoundException($"Presente {presentId} não encontrado.");

            var request = new PreferenceRequest
            {
                Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Title = present.NamePresent,
                        Quantity = 1,
                        CurrencyId = "BRL",
                        UnitPrice = present.PricePresent
                    }
                },
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = $"{_frontendSettings.BaseUrl}/pagamento/sucesso",
                    Failure = $"{_frontendSettings.BaseUrl}/pagamento/erro",
                    Pending = $"{_frontendSettings.BaseUrl}/pagamento/pendente"
                },
                AutoReturn = "approved",
                ExternalReference = presentId.ToString(),
                NotificationUrl = string.IsNullOrEmpty(_mpSettings.NotificationUrl)
                    ? null
                    : _mpSettings.NotificationUrl
            };

            var client = new PreferenceClient();
            var preference = await client.CreateAsync(request);

            return new PreferenceResponseDto
            {
                InitPoint = preference.InitPoint,
                PreferenceId = preference.Id
            };
        }

        public async Task ProcessarWebhookAsync(string type, string dataId)
        {
            if (type != "payment")
            {
                _logger.LogInformation("Notificação ignorada, tipo: {Type}", type);
                return;
            }

            var paymentClient = new PaymentClient();
            Payment payment = await paymentClient.GetAsync(long.Parse(dataId));

            if (string.IsNullOrEmpty(payment.ExternalReference) ||
                !Guid.TryParse(payment.ExternalReference, out var presentId))
            {
                _logger.LogWarning("ExternalReference inválido no pagamento {PaymentId}", payment.Id);
                return;
            }

            switch (payment.Status)
            {
                case "approved":
                    await _presentService.MarcarComoPagoAsync(presentId, payment.Id.ToString());
                    break;
                case "pending":
                case "in_process":
                    await _presentService.MarcarComoPendenteAsync(presentId);
                    break;
                case "rejected":
                case "cancelled":
                    await _presentService.MarcarComoRejeitadoAsync(presentId);
                    break;
                default:
                    _logger.LogInformation("Status não tratado: {Status}", payment.Status);
                    break;
            }
        }

        // Valida o header x-signature enviado pelo Mercado Pago,
        // garantindo que a chamada ao webhook realmente veio de lá.
        public bool ValidarAssinaturaWebhook(string? xSignature, string? xRequestId, string dataId)
        {
            if (string.IsNullOrEmpty(xSignature) || string.IsNullOrEmpty(_mpSettings.WebhookSecret))
                return false;

            var parts = xSignature.Split(',')
                .Select(p => p.Split('='))
                .Where(p => p.Length == 2)
                .ToDictionary(p => p[0].Trim(), p => p[1].Trim());

            if (!parts.TryGetValue("ts", out var ts) || !parts.TryGetValue("v1", out var v1))
                return false;

            var manifest = $"id:{dataId};request-id:{xRequestId};ts:{ts};";

            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_mpSettings.WebhookSecret));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(manifest));
            var hashHex = Convert.ToHexString(hash).ToLowerInvariant();

            return hashHex == v1;
        }
    }
}