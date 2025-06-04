using Mapster;
using Microsoft.EntityFrameworkCore;
using Solution1.Domain.DTOs;
using Solution1.Domain.Entities;
using Solution1.Domain.Interfaces;
using Solution1.Infra.Data;

namespace Solution1.Infra.Services;

public class FornecedorService : IService<FornecedorDto, CreateFornecedorDto, UpdateFornecedorDto>
{
    private readonly IRepository<Fornecedor> _repository;
    private readonly ApplicationDbContext _context;

    public FornecedorService(IRepository<Fornecedor> repository, ApplicationDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<IEnumerable<FornecedorDto>> GetAllAsync()
    {
        var fornecedores = await _repository.GetAllAsync();
        return fornecedores.Adapt<IEnumerable<FornecedorDto>>();
    }

    public async Task<FornecedorDto> GetByIdAsync(Guid id)
    {
        var fornecedor = await _repository.GetByIdAsync(id);
        if (fornecedor == null)
            throw new KeyNotFoundException($"Fornecedor com ID {id} não encontrado.");

        return fornecedor.Adapt<FornecedorDto>();
    }

    public async Task<FornecedorDto> CreateAsync(CreateFornecedorDto dto)
    {
        await ValidateBusinessRulesAsync(dto);

        var fornecedor = dto.Adapt<Fornecedor>();
        var result = await _repository.AddAsync(fornecedor);
        return result.Adapt<FornecedorDto>();
    }

    public async Task<FornecedorDto> UpdateAsync(Guid id, UpdateFornecedorDto dto)
    {
        var fornecedor = await _repository.GetByIdAsync(id);
        if (fornecedor == null)
            throw new KeyNotFoundException($"Fornecedor com ID {id} não encontrado.");

        await ValidateBusinessRulesAsync(dto, id);

        dto.Adapt(fornecedor);
        var result = await _repository.UpdateAsync(fornecedor);
        return result.Adapt<FornecedorDto>();
    }

    public async Task DeleteAsync(Guid id)
    {
        var fornecedor = await _repository.GetByIdAsync(id);
        if (fornecedor == null)
            throw new KeyNotFoundException($"Fornecedor com ID {id} não encontrado.");

        var fornecedorEmUso = await _context.ContratosVenda.AnyAsync(c => c.FornecedorId == id);
        if (fornecedorEmUso)
            throw new InvalidOperationException("Fornecedor possui contratos e não pode ser excluído.");

        await _repository.DeleteAsync(id);
    }

    private async Task ValidateBusinessRulesAsync(CreateFornecedorDto dto, Guid? excludeId = null)
    {
        var cnpjExists = await _context.Fornecedores
            .AnyAsync(f => f.Cnpj == dto.Cnpj && (excludeId == null || f.Id != excludeId));
        if (cnpjExists)
            throw new ArgumentException("Já existe um fornecedor com este CNPJ.");

        var codigoExists = await _context.Fornecedores
            .AnyAsync(f => f.Codigo == dto.Codigo && (excludeId == null || f.Id != excludeId));
        if (codigoExists)
            throw new ArgumentException("Já existe um fornecedor com este código.");
    }

    private async Task ValidateBusinessRulesAsync(UpdateFornecedorDto dto, Guid excludeId)
    {
        await ValidateBusinessRulesAsync(new CreateFornecedorDto
        {
            Codigo = dto.Codigo,
            Descricao = dto.Descricao,
            Cnpj = dto.Cnpj
        }, excludeId);
    }
}