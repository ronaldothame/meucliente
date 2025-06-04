using System.ComponentModel.DataAnnotations;
using Solution1.Domain.Interfaces;

namespace Solution1.Domain.DTOs;

public class AtivoDto : IEntityDto
{
    public Guid Id { get; set; }
    public string Codigo { get; set; }
    public string Descricao { get; set; }
    public Guid TipoAtivoId { get; set; }
    public decimal PrecoVenda { get; set; }
    public string? TipoAtivoDescricao { get; set; }
}

public class CreateAtivoDto
{
    [Required(ErrorMessage = "Código do ativo é obrigatório.")]
    [StringLength(50, ErrorMessage = "Código do ativo deve ter no máximo 50 caracteres.")]
    public string Codigo { get; set; }

    [Required(ErrorMessage = "Descrição do ativo é obrigatória.")]
    [StringLength(200, ErrorMessage = "Descrição do ativo deve ter no máximo 200 caracteres.")]
    public string Descricao { get; set; }

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
    public string Codigo { get; set; }

    [Required(ErrorMessage = "Descrição do ativo é obrigatória.")]
    [StringLength(200, ErrorMessage = "Descrição do ativo deve ter no máximo 200 caracteres.")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "ID do Tipo de Ativo é obrigatório.")]
    public Guid TipoAtivoId { get; set; }

    [Required(ErrorMessage = "Preço de venda é obrigatório.")]
    [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Preço de venda deve ser maior que zero.")]
    public decimal PrecoVenda { get; set; }
}