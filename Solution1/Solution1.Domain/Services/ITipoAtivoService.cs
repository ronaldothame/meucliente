using Solution1.Domain.DTOs;

namespace Solution1.Domain.Services;

public interface ITipoAtivoService
{
    Task<IEnumerable<TipoAtivoDto>> GetAllAsync();
    Task<TipoAtivoDto?> GetByIdAsync(Guid id);
    Task<TipoAtivoDto> CreateAsync(CreateTipoAtivoDto dto);
    Task<TipoAtivoDto?> UpdateAsync(Guid id, UpdateTipoAtivoDto dto);
    Task<bool> DeleteAsync(Guid id);
}