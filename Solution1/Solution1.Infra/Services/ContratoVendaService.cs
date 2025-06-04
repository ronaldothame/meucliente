using Mapster;
using Microsoft.EntityFrameworkCore;
using Solution1.Domain.DTOs;
using Solution1.Domain.Entities;
using Solution1.Domain.Interfaces;
using Solution1.Infra.Data;

namespace Solution1.Infra.Services;

public class ContratoVendaService : IService<ContratoVendaDto, CreateContratoVendaDto, UpdateContratoVendaDto>
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

    public async Task<ContratoVendaDto> GetByIdAsync(Guid id)
    {
        var contrato = await _context.ContratosVenda
            .Include(c => c.Fornecedor)
            .Include(c => c.Itens)
            .ThenInclude(i => i.Ativo)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (contrato == null)
            throw new KeyNotFoundException($"Contrato de venda com ID {id} não encontrado.");

        return contrato.Adapt<ContratoVendaDto>();
    }

    public async Task<ContratoVendaDto> CreateAsync(CreateContratoVendaDto dto)
    {
        await ValidateBusinessRulesAsync(dto);

        var contrato = new ContratoVenda
        {
            NumeroContrato = dto.NumeroContrato,
            FornecedorId = dto.FornecedorId,
            Desconto = dto.Desconto,
            DataCriacao = DateTime.UtcNow,
            DataAlteracao = DateTime.UtcNow,
            Itens = new List<ItemContrato>()
        };

        foreach (var itemDto in dto.Itens)
        {
            var item = new ItemContrato
            {
                AtivoId = itemDto.AtivoId,
                Quantidade = itemDto.Quantidade,
                PrecoUnitario = itemDto.PrecoUnitario,
                ValorTotal = itemDto.Quantidade * itemDto.PrecoUnitario,
                ContratoVendaId = contrato.Id
            };
            contrato.Itens.Add(item);
        }

        var subtotal = contrato.Itens.Sum(i => i.ValorTotal);
        contrato.ValorTotal = subtotal - contrato.Desconto;

        var result = await _repository.AddAsync(contrato);

        var contratoCompleto = await _context.ContratosVenda
            .Include(c => c.Fornecedor)
            .Include(c => c.Itens)
            .ThenInclude(i => i.Ativo)
            .FirstAsync(c => c.Id == result.Id);

        return contratoCompleto.Adapt<ContratoVendaDto>();
    }

    public async Task<ContratoVendaDto> UpdateAsync(Guid id, UpdateContratoVendaDto dto)
    {
        var contrato = await _context.ContratosVenda
            .Include(c => c.Itens)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (contrato == null)
            throw new KeyNotFoundException($"Contrato de venda com ID {id} não encontrado.");

        await ValidateBusinessRulesAsync(dto, id);

        contrato.Itens.Clear();

        contrato.NumeroContrato = dto.NumeroContrato;
        contrato.FornecedorId = dto.FornecedorId;
        contrato.Desconto = dto.Desconto;
        contrato.DataAlteracao = DateTime.UtcNow;

        foreach (var itemDto in dto.Itens)
        {
            var item = new ItemContrato
            {
                AtivoId = itemDto.AtivoId,
                Quantidade = itemDto.Quantidade,
                PrecoUnitario = itemDto.PrecoUnitario,
                ValorTotal = itemDto.Quantidade * itemDto.PrecoUnitario,
                ContratoVendaId = contrato.Id
            };
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

    public async Task DeleteAsync(Guid id)
    {
        var contrato = await _repository.GetByIdAsync(id);
        if (contrato == null)
            throw new KeyNotFoundException($"Contrato de venda com ID {id} não encontrado.");

        await _repository.DeleteAsync(id);
    }

    private async Task ValidateBusinessRulesAsync(CreateContratoVendaDto dto, Guid? excludeId = null)
    {
        var numeroExists = await _context.ContratosVenda
            .AnyAsync(c => c.NumeroContrato == dto.NumeroContrato && (excludeId == null || c.Id != excludeId));
        if (numeroExists)
            throw new ArgumentException("Já existe um contrato com este número.");

        var fornecedorExists = await _context.Fornecedores.AnyAsync(f => f.Id == dto.FornecedorId);
        if (!fornecedorExists)
            throw new ArgumentException("Fornecedor não encontrado.");

        var ativosIds = dto.Itens.Select(i => i.AtivoId).Distinct().ToList();
        var ativosExistentes = await _context.Ativos
            .Where(a => ativosIds.Contains(a.Id))
            .CountAsync();

        if (ativosExistentes != ativosIds.Count)
            throw new ArgumentException("Um ou mais ativos não foram encontrados.");

        var itensDuplicados = dto.Itens.GroupBy(i => i.AtivoId).Any(g => g.Count() > 1);
        if (itensDuplicados)
            throw new ArgumentException("Não é permitido ter itens duplicados no contrato.");
    }

    private async Task ValidateBusinessRulesAsync(UpdateContratoVendaDto dto, Guid excludeId)
    {
        await ValidateBusinessRulesAsync(new CreateContratoVendaDto
        {
            NumeroContrato = dto.NumeroContrato,
            FornecedorId = dto.FornecedorId,
            Desconto = dto.Desconto,
            Itens = dto.Itens
        }, excludeId);
    }
}