using System.ComponentModel.DataAnnotations;

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
    [Required(ErrorMessage = "ID do Ativo é obrigatório para o item.")]
    public Guid AtivoId { get; set; }

    [Required(ErrorMessage = "Quantidade é obrigatória para o item.")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser pelo menos 1.")]
    public int Quantidade { get; set; }

    [Required(ErrorMessage = "Preço unitário é obrigatório para o item.")]
    [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Preço unitário deve ser maior que zero.")]
    public decimal PrecoUnitario { get; set; }
}