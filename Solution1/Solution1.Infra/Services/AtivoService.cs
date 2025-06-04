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

    public async Task<AtivoDto?> GetByIdAsync(Guid id)
    {
        var ativo = await _context.Ativos
            .Include(a => a.TipoAtivo)
            .FirstOrDefaultAsync(a => a.Id == id);
        return ativo?.Adapt<AtivoDto>();
    }

    public async Task<AtivoDto> CreateAsync(CreateAtivoDto dto)
    {
        var ativo = dto.Adapt<Ativo>();
        var result = await _repository.AddAsync(ativo);

        var ativoCompleto = await _context.Ativos
            .Include(a => a.TipoAtivo)
            .FirstAsync(a => a.Id == result.Id);

        return ativoCompleto.Adapt<AtivoDto>();
    }

    public async Task<AtivoDto?> UpdateAsync(Guid id, UpdateAtivoDto dto)
    {
        var ativo = await _repository.GetByIdAsync(id);
        if (ativo == null) return null;

        dto.Adapt(ativo);

        await _repository.UpdateAsync(ativo);

        var ativoCompleto = await _context.Ativos
            .Include(a => a.TipoAtivo)
            .FirstAsync(a => a.Id == id);

        return ativoCompleto.Adapt<AtivoDto>();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var ativo = await _repository.GetByIdAsync(id);
        if (ativo == null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}