namespace Solution1.Domain.DTOs;

public class ContratoVendaDto
{
    public Guid Id { get; set; }
    public string NumeroContrato { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAlteracao { get; set; }
    public Guid FornecedorId { get; set; }
    public string? FornecedorDescricao { get; set; }
    public decimal Desconto { get; set; }
    public decimal ValorTotal { get; set; }
    public List<ItemContratoDto> Itens { get; set; } = new();
}

public class CreateContratoVendaDto
{
    public string NumeroContrato { get; set; }
    public Guid FornecedorId { get; set; }
    public decimal Desconto { get; set; }
    public List<CreateItemContratoDto> Itens { get; set; } = new();
}

public class UpdateContratoVendaDto
{
    public string NumeroContrato { get; set; }
    public Guid FornecedorId { get; set; }
    public decimal Desconto { get; set; }
    public List<CreateItemContratoDto> Itens { get; set; } = new();
}