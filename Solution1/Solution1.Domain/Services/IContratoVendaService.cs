using Solution1.Domain.DTOs;

namespace Solution1.Domain.Services;

public interface IContratoVendaService
{
    Task<IEnumerable<ContratoVendaDto>> GetAllAsync();
    Task<ContratoVendaDto?> GetByIdAsync(Guid id);
    Task<ContratoVendaDto> CreateAsync(CreateContratoVendaDto dto);
    Task<ContratoVendaDto?> UpdateAsync(Guid id, UpdateContratoVendaDto dto);
    Task<bool> DeleteAsync(Guid id);
}