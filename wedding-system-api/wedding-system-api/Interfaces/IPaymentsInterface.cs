using wedding_system_api.Dtos;

namespace wedding_system_api.InterfaceServices
{
    public interface IPaymentInterface
    {
        Task<PreferenceResponseDto> CriarPreferenciaAsync(Guid presentId);
        Task ProcessarWebhookAsync(string type, string dataId);
        bool ValidarAssinaturaWebhook(string? xSignature, string? xRequestId, string dataId);
    }
}