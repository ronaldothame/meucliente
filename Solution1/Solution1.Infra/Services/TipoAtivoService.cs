using Mapster;
using Solution1.Domain.DTOs;
using Solution1.Domain.Entities;
using Solution1.Domain.Interfaces;

namespace Solution1.Infra.Services;

public class TipoAtivoService : IService<TipoAtivoDto, CreateTipoAtivoDto, UpdateTipoAtivoDto>
{
    private readonly IRepository<TipoAtivo> _repository;

    public TipoAtivoService(IRepository<TipoAtivo> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TipoAtivoDto>> GetAllAsync()
    {
        var tiposAtivo = await _repository.GetAllAsync();
        return tiposAtivo.Adapt<IEnumerable<TipoAtivoDto>>();
    }

    public async Task<TipoAtivoDto?> GetByIdAsync(Guid id)
    {
        var tipoAtivo = await _repository.GetByIdAsync(id);
        return tipoAtivo?.Adapt<TipoAtivoDto>();
    }

    public async Task<TipoAtivoDto> CreateAsync(CreateTipoAtivoDto dto)
    {
        var tipoAtivo = dto.Adapt<TipoAtivo>();
        var result = await _repository.AddAsync(tipoAtivo);
        return result.Adapt<TipoAtivoDto>();
    }

    public async Task<TipoAtivoDto?> UpdateAsync(Guid id, UpdateTipoAtivoDto dto)
    {
        var tipoAtivo = await _repository.GetByIdAsync(id);
        if (tipoAtivo == null) return null;

        dto.Adapt(tipoAtivo);

        var result = await _repository.UpdateAsync(tipoAtivo);
        return result.Adapt<TipoAtivoDto>();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var tipoAtivo = await _repository.GetByIdAsync(id);
        if (tipoAtivo == null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}