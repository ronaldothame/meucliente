using Solution1.Domain.DTOs;

namespace Solution1.Domain.Services;

public interface IFornecedorService
{
    Task<IEnumerable<FornecedorDto>> GetAllAsync();
    Task<FornecedorDto?> GetByIdAsync(Guid id);
    Task<FornecedorDto> CreateAsync(CreateFornecedorDto dto);
    Task<FornecedorDto?> UpdateAsync(Guid id, UpdateFornecedorDto dto);
    Task<bool> DeleteAsync(Guid id);
}