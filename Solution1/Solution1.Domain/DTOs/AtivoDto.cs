using System.ComponentModel.DataAnnotations;

namespace Solution1.Domain.DTOs;

public class AtivoDto
{
    public Guid Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public Guid TipoAtivoId { get; set; }
    public decimal PrecoVenda { get; set; }
    public string? TipoAtivoDescricao { get; set; }
}

public class CreateAtivoDto
{
    [Required(ErrorMessage = "Código do ativo é obrigatório.")]
    [StringLength(50, ErrorMessage = "Código do ativo deve ter no máximo 50 caracteres.")]
    public string Codigo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição do ativo é obrigatória.")]
    [StringLength(200, ErrorMessage = "Descrição do ativo deve ter no máximo 200 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "ID do Tipo de Ativo é obrigatório.")]
    public Guid TipoAtivoId { get; set; }

    [Required(ErrorMessage = "Preço de venda é obrigatório.")]
    [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Preço de venda deve ser maior que zero.")]
    public decimal PrecoVenda { get; set; }
}

public class UpdateAtivoDto
{
    [Required(ErrorMessage = "Código do ativo é obrigatório.")]
    [StringLength(50, ErrorMessage = "Código do ativo deve ter no máximo 50 caracteres.")]
    public string Codigo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição do ativo é obrigatória.")]
    [StringLength(200, ErrorMessage = "Descrição do ativo deve ter no máximo 200 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "ID do Tipo de Ativo é obrigatório.")]
    public Guid TipoAtivoId { get; set; }

    [Required(ErrorMessage = "Preço de venda é obrigatório.")]
    [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Preço de venda deve ser maior que zero.")]
    public decimal PrecoVenda { get; set; }
}