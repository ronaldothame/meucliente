using Mapster;
using Solution1.Domain.DTOs;
using Solution1.Domain.Entities;
using Solution1.Domain.Interfaces;
using Solution1.Domain.Services;

namespace Solution1.Infra.Services;

public class FornecedorService : IFornecedorService
{
    private readonly IRepository<Fornecedor> _repository;

    public FornecedorService(IRepository<Fornecedor> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<FornecedorDto>> GetAllAsync()
    {
        var fornecedores = await _repository.GetAllAsync();
        return fornecedores.Adapt<IEnumerable<FornecedorDto>>();
    }

    public async Task<FornecedorDto?> GetByIdAsync(Guid id)
    {
        var fornecedor = await _repository.GetByIdAsync(id);
        return fornecedor?.Adapt<FornecedorDto>();
    }

    public async Task<FornecedorDto> CreateAsync(CreateFornecedorDto dto)
    {
        var fornecedor = dto.Adapt<Fornecedor>();
        var result = await _repository.AddAsync(fornecedor);
        return result.Adapt<FornecedorDto>();
    }

    public async Task<FornecedorDto?> UpdateAsync(Guid id, UpdateFornecedorDto dto)
    {
        var fornecedor = await _repository.GetByIdAsync(id);
        if (fornecedor == null) return null;

        dto.Adapt(fornecedor);
        var result = await _repository.UpdateAsync(fornecedor);
        return result.Adapt<FornecedorDto>();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var fornecedor = await _repository.GetByIdAsync(id);
        if (fornecedor == null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}