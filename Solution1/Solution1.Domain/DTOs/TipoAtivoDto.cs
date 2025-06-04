using System.ComponentModel.DataAnnotations;
using Solution1.Domain.Interfaces;

namespace Solution1.Domain.DTOs;

public class TipoAtivoDto : IEntityDto
{
    public Guid Id { get; set; }
    public string Codigo { get; set; }
    public string Descricao { get; set; }
}

public class CreateTipoAtivoDto
{
    [Required(ErrorMessage = "Código do tipo de ativo é obrigatório.")]
    [StringLength(50, ErrorMessage = "Código do tipo de ativo deve ter no máximo 50 caracteres.")]
    public string Codigo { get; set; }

    [Required(ErrorMessage = "Descrição do tipo de ativo é obrigatória.")]
    [StringLength(200, ErrorMessage = "Descrição do tipo de ativo deve ter no máximo 200 caracteres.")]
    public string Descricao { get; set; }
}

public class UpdateTipoAtivoDto
{
    [Required(ErrorMessage = "Código do tipo de ativo é obrigatório.")]
    [StringLength(50, ErrorMessage = "Código do tipo de ativo deve ter no máximo 50 caracteres.")]
    public string Codigo { get; set; }

    [Required(ErrorMessage = "Descrição do tipo de ativo é obrigatória.")]
    [StringLength(200, ErrorMessage = "Descrição do tipo de ativo deve ter no máximo 200 caracteres.")]
    public string Descricao { get; set; }
}