using wedding_system_api.Dtos;

namespace wedding_system_api.InterfaceServices
{
    public interface IPaymentInterface
    {
        // Cria o pagamento para um presente e devolve os dados do checkout.
        // Retorna null se o presente não existir.
        Task<CreatePaymentResponseDto?> CreateCheckoutAsync(Guid presentId);
    }
}