using Solution1.Domain.DTOs;

namespace Solution1.Domain.Services;

public interface IAtivoService
{
    Task<IEnumerable<AtivoDto>> GetAllAsync();
    Task<AtivoDto?> GetByIdAsync(Guid id);
    Task<AtivoDto> CreateAsync(CreateAtivoDto dto);
    Task<AtivoDto?> UpdateAsync(Guid id, UpdateAtivoDto dto);
    Task<bool> DeleteAsync(Guid id);
}