using wedding_system_api.Dtos;
using wedding_system_api.Models;

namespace wedding_system_api.InterfaceServices
{
    public interface IPresentInterface
    {
        Task<IEnumerable<PresentResponseDto>> GetAllAsync();
        Task<PresentResponseDto?> GetByIdAsync(Guid id);
        Task<PresentResponseDto> CreateAsync(CreatePresentDto criarPresenteDto);
        Task<bool> UpdateAsync(Guid id, presentsModel present);
        Task<bool> DeleteAsync(Guid id);

        Task MarcarComoPendenteAsync(Guid presentId);
        Task MarcarComoPagoAsync(Guid presentId, string paymentId);
        Task MarcarComoRejeitadoAsync(Guid presentId);
        Task<bool> PagamentoJaProcessadoAsync(string paymentId);
        Task<StatusPagamento?> GetStatusAsync(Guid presentId);
    }
}