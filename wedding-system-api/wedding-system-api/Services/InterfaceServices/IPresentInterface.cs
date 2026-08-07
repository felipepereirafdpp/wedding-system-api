using wedding_system_api.Models;

namespace wedding_system_api.Services.InterfaceServices
{
    public interface IPresentInterface
    {
        Task<IEnumerable<presentsModel>> GetAllAsync();
        Task<presentsModel?> GetByIdAsync(Guid id);
        Task<presentsModel> CreateAsync(presentsModel present);
        Task<bool> UpdateAsync(Guid id, presentsModel present);
        Task<bool> DeleteAsync(Guid id);
    }
}
