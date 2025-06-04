using Mapster;
using Microsoft.EntityFrameworkCore;
using Solution1.Domain.DTOs;
using Solution1.Domain.Entities;
using Solution1.Domain.Interfaces;
using Solution1.Infra.Data;

namespace Solution1.Infra.Services;

public class AtivoService : IService<AtivoDto, CreateAtivoDto, UpdateAtivoDto>
{
    private readonly IRepository<Ativo> _repository;
    private readonly ApplicationDbContext _context;

    public AtivoService(IRepository<Ativo> repository, ApplicationDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<IEnumerable<AtivoDto>> GetAllAsync()
    {
        var ativos = await _context.Ativos
            .Include(a => a.TipoAtivo)
            .ToListAsync();
        return ativos.Adapt<IEnumerable<AtivoDto>>();
    }

    public async Task<AtivoDto> GetByIdAsync(Guid id)
    {
        var ativo = await _context.Ativos
            .Include(a => a.TipoAtivo)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (ativo == null)
            throw new KeyNotFoundException($"Ativo com ID {id} não encontrado.");

        return ativo.Adapt<AtivoDto>();
    }

    public async Task<AtivoDto> CreateAsync(CreateAtivoDto dto)
    {
        await ValidateBusinessRulesAsync(dto);

        var ativo = dto.Adapt<Ativo>();
        var result = await _repository.AddAsync(ativo);

        var ativoCompleto = await _context.Ativos
            .Include(a => a.TipoAtivo)
            .FirstAsync(a => a.Id == result.Id);

        return ativoCompleto.Adapt<AtivoDto>();
    }

    public async Task<AtivoDto> UpdateAsync(Guid id, UpdateAtivoDto dto)
    {
        var ativo = await _repository.GetByIdAsync(id);
        if (ativo == null)
            throw new KeyNotFoundException($"Ativo com ID {id} não encontrado.");

        await ValidateBusinessRulesAsync(dto, id);

        dto.Adapt(ativo);
        await _repository.UpdateAsync(ativo);

        var ativoCompleto = await _context.Ativos
            .Include(a => a.TipoAtivo)
            .FirstAsync(a => a.Id == id);

        return ativoCompleto.Adapt<AtivoDto>();
    }

    public async Task DeleteAsync(Guid id)
    {
        var ativo = await _repository.GetByIdAsync(id);
        if (ativo == null)
            throw new KeyNotFoundException($"Ativo com ID {id} não encontrado.");

        var ativoEmUso = await _context.ItensContrato.AnyAsync(i => i.AtivoId == id);
        if (ativoEmUso)
            throw new InvalidOperationException("Ativo está sendo usado em contratos e não pode ser excluído.");

        await _repository.DeleteAsync(id);
    }

    private async Task ValidateBusinessRulesAsync(CreateAtivoDto dto, Guid? excludeId = null)
    {
        var tipoAtivoExists = await _context.TiposAtivo.AnyAsync(t => t.Id == dto.TipoAtivoId);
        if (!tipoAtivoExists)
            throw new ArgumentException("Tipo de ativo não encontrado.");

        var codigoExists = await _context.Ativos
            .AnyAsync(a => a.Codigo == dto.Codigo && (excludeId == null || a.Id != excludeId));
        if (codigoExists)
            throw new ArgumentException("Já existe um ativo com este código.");
    }

    private async Task ValidateBusinessRulesAsync(UpdateAtivoDto dto, Guid excludeId)
    {
        await ValidateBusinessRulesAsync(new CreateAtivoDto
        {
            Codigo = dto.Codigo,
            Descricao = dto.Descricao,
            TipoAtivoId = dto.TipoAtivoId,
            PrecoVenda = dto.PrecoVenda
        }, excludeId);
    }
}