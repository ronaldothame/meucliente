using System.ComponentModel.DataAnnotations;

namespace Solution1.Domain.DTOs;

public class TipoAtivoDto
{
    public Guid Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
}

public class CreateTipoAtivoDto
{
    [Required(ErrorMessage = "Código do tipo de ativo é obrigatório.")]
    [StringLength(50, ErrorMessage = "Código do tipo de ativo deve ter no máximo 50 caracteres.")]
    public string Codigo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição do tipo de ativo é obrigatória.")]
    [StringLength(200, ErrorMessage = "Descrição do tipo de ativo deve ter no máximo 200 caracteres.")]
    public string Descricao { get; set; } = string.Empty;
}

public class UpdateTipoAtivoDto
{
    [Required(ErrorMessage = "Código do tipo de ativo é obrigatório.")]
    [StringLength(50, ErrorMessage = "Código do tipo de ativo deve ter no máximo 50 caracteres.")]
    public string Codigo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Descrição do tipo de ativo é obrigatória.")]
    [StringLength(200, ErrorMessage = "Descrição do tipo de ativo deve ter no máximo 200 caracteres.")]
    public string Descricao { get; set; } = string.Empty;
}