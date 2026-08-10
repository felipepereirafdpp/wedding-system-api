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
    }
}