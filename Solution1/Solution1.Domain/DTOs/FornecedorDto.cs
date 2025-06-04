using System.ComponentModel.DataAnnotations;
using Solution1.Domain.Interfaces;
using Solution1.Domain.ValidationAttributes;

namespace Solution1.Domain.DTOs;

public class FornecedorDto : IEntityDto
{
    public Guid Id { get; set; }
    public string Codigo { get; set; }
    public string Descricao { get; set; }
    public string Cnpj { get; set; }
}

public class CreateFornecedorDto
{
    [Required(ErrorMessage = "Código do fornecedor é obrigatório.")]
    [StringLength(50, ErrorMessage = "Código do fornecedor deve ter no máximo 50 caracteres.")]
    public string Codigo { get; set; }

    [Required(ErrorMessage = "Descrição do fornecedor é obrigatória.")]
    [StringLength(200, ErrorMessage = "Descrição do fornecedor deve ter no máximo 200 caracteres.")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "CNPJ é obrigatório.")]
    [CnpjValidation(ErrorMessage = "O CNPJ informado é inválido.")]
    public string Cnpj { get; set; }
}

public class UpdateFornecedorDto
{
    [Required(ErrorMessage = "Código do fornecedor é obrigatório.")]
    [StringLength(50, ErrorMessage = "Código do fornecedor deve ter no máximo 50 caracteres.")]
    public string Codigo { get; set; }

    [Required(ErrorMessage = "Descrição do fornecedor é obrigatória.")]
    [StringLength(200, ErrorMessage = "Descrição do fornecedor deve ter no máximo 200 caracteres.")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "CNPJ é obrigatório.")]
    [CnpjValidation(ErrorMessage = "O CNPJ informado é inválido.")]
    public string Cnpj { get; set; }
}