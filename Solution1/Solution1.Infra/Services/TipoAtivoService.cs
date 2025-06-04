using Mapster;
using Microsoft.EntityFrameworkCore;
using Solution1.Domain.DTOs;
using Solution1.Domain.Entities;
using Solution1.Domain.Interfaces;
using Solution1.Infra.Data;

namespace Solution1.Infra.Services;

public class TipoAtivoService : IService<TipoAtivoDto, CreateTipoAtivoDto, UpdateTipoAtivoDto>
{
    private readonly IRepository<TipoAtivo> _repository;
    private readonly ApplicationDbContext _context;

    public TipoAtivoService(IRepository<TipoAtivo> repository, ApplicationDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<IEnumerable<TipoAtivoDto>> GetAllAsync()
    {
        var tiposAtivo = await _repository.GetAllAsync();
        return tiposAtivo.Adapt<IEnumerable<TipoAtivoDto>>();
    }

    public async Task<TipoAtivoDto> GetByIdAsync(Guid id)
    {
        var tipoAtivo = await _repository.GetByIdAsync(id);
        if (tipoAtivo == null)
            throw new KeyNotFoundException($"Tipo de ativo com ID {id} não encontrado.");

        return tipoAtivo.Adapt<TipoAtivoDto>();
    }

    public async Task<TipoAtivoDto> CreateAsync(CreateTipoAtivoDto dto)
    {
        await ValidateBusinessRulesAsync(dto);

        var tipoAtivo = dto.Adapt<TipoAtivo>();
        var result = await _repository.AddAsync(tipoAtivo);
        return result.Adapt<TipoAtivoDto>();
    }

    public async Task<TipoAtivoDto> UpdateAsync(Guid id, UpdateTipoAtivoDto dto)
    {
        var tipoAtivo = await _repository.GetByIdAsync(id);
        if (tipoAtivo == null)
            throw new KeyNotFoundException($"Tipo de ativo com ID {id} não encontrado.");

        await ValidateBusinessRulesAsync(dto, id);

        dto.Adapt(tipoAtivo);
        var result = await _repository.UpdateAsync(tipoAtivo);
        return result.Adapt<TipoAtivoDto>();
    }

    public async Task DeleteAsync(Guid id)
    {
        var tipoAtivo = await _repository.GetByIdAsync(id);
        if (tipoAtivo == null)
            throw new KeyNotFoundException($"Tipo de ativo com ID {id} não encontrado.");

        var tipoAtivoEmUso = await _context.Ativos.AnyAsync(a => a.TipoAtivoId == id);
        if (tipoAtivoEmUso)
            throw new InvalidOperationException("Tipo de ativo possui ativos associados e não pode ser excluído.");

        await _repository.DeleteAsync(id);
    }

    private async Task ValidateBusinessRulesAsync(CreateTipoAtivoDto dto, Guid? excludeId = null)
    {
        var codigoExists = await _context.TiposAtivo
            .AnyAsync(t => t.Codigo == dto.Codigo && (excludeId == null || t.Id != excludeId));
        if (codigoExists)
            throw new ArgumentException("Já existe um tipo de ativo com este código.");
    }

    private async Task ValidateBusinessRulesAsync(UpdateTipoAtivoDto dto, Guid excludeId)
    {
        await ValidateBusinessRulesAsync(new CreateTipoAtivoDto
        {
            Codigo = dto.Codigo,
            Descricao = dto.Descricao
        }, excludeId);
    }
}