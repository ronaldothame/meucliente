namespace Solution1.Domain.DTOs;

public class ItemContratoDto
{
    public Guid Id { get; set; }
    public Guid AtivoId { get; set; }
    public string? AtivoDescricao { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal ValorTotal => Quantidade * PrecoUnitario;
}

public class CreateItemContratoDto
{
    public Guid AtivoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
}