using Mapster;
using Microsoft.EntityFrameworkCore;
using Solution1.Domain.DTOs;
using Solution1.Domain.Entities;
using Solution1.Domain.Interfaces;
using Solution1.Domain.Services;
using Solution1.Infra.Data;

namespace Solution1.Infra.Services;

public class ContratoVendaService : IContratoVendaService
{
    private readonly IRepository<ContratoVenda> _repository;
    private readonly ApplicationDbContext _context;

    public ContratoVendaService(IRepository<ContratoVenda> repository, ApplicationDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<IEnumerable<ContratoVendaDto>> GetAllAsync()
    {
        var contratos = await _context.ContratosVenda
            .Include(c => c.Fornecedor)
            .Include(c => c.Itens)
            .ThenInclude(i => i.Ativo)
            .ToListAsync();
        return contratos.Adapt<IEnumerable<ContratoVendaDto>>();
    }

    public async Task<ContratoVendaDto?> GetByIdAsync(Guid id)
    {
        var contrato = await _context.ContratosVenda
            .Include(c => c.Fornecedor)
            .Include(c => c.Itens)
            .ThenInclude(i => i.Ativo)
            .FirstOrDefaultAsync(c => c.Id == id);
        return contrato?.Adapt<ContratoVendaDto>();
    }

    public async Task<ContratoVendaDto> CreateAsync(CreateContratoVendaDto dto)
    {
        var contrato = dto.Adapt<ContratoVenda>();
        contrato.DataCriacao = DateTime.UtcNow;
        contrato.DataAlteracao = DateTime.UtcNow;

        if (dto.Itens?.Any() == true)
        {
            foreach (var itemDto in dto.Itens)
            {
                var item = itemDto.Adapt<ItemContrato>();
                item.ContratoVendaId = contrato.Id;
                item.ValorTotal = item.Quantidade * item.PrecoUnitario;
                contrato.Itens.Add(item);
            }

            var subtotal = contrato.Itens.Sum(i => i.ValorTotal);
            contrato.ValorTotal = subtotal - contrato.Desconto;
        }
        else
        {
            contrato.ValorTotal = -contrato.Desconto;
        }

        var result = await _repository.AddAsync(contrato);

        var contratoCompleto = await _context.ContratosVenda
            .Include(c => c.Fornecedor)
            .Include(c => c.Itens)
            .ThenInclude(i => i.Ativo)
            .FirstAsync(c => c.Id == result.Id);

        return contratoCompleto.Adapt<ContratoVendaDto>();
    }

    public async Task<ContratoVendaDto?> UpdateAsync(Guid id, UpdateContratoVendaDto dto)
    {
        var contrato = await _context.ContratosVenda
            .Include(c => c.Itens)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (contrato == null) return null;

        contrato.Itens.Clear();

        dto.Adapt(contrato);
        contrato.DataAlteracao = DateTime.UtcNow;

        if (dto.Itens?.Any() == true)
            foreach (var itemDto in dto.Itens)
            {
                var item = itemDto.Adapt<ItemContrato>();
                item.ContratoVendaId = contrato.Id;
                item.ValorTotal = item.Quantidade * item.PrecoUnitario;
                contrato.Itens.Add(item);
            }

        var subtotal = contrato.Itens.Sum(i => i.ValorTotal);
        contrato.ValorTotal = subtotal - contrato.Desconto;

        await _repository.UpdateAsync(contrato);

        var contratoCompleto = await _context.ContratosVenda
            .Include(c => c.Fornecedor)
            .Include(c => c.Itens)
            .ThenInclude(i => i.Ativo)
            .FirstAsync(c => c.Id == id);

        return contratoCompleto.Adapt<ContratoVendaDto>();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var contrato = await _repository.GetByIdAsync(id);
        if (contrato == null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}