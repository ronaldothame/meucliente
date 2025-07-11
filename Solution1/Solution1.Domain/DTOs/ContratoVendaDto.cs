﻿using System.ComponentModel.DataAnnotations;
using Solution1.Domain.Interfaces;

namespace Solution1.Domain.DTOs;

public class ContratoVendaDto : IEntityDto
{
    public Guid Id { get; set; }
    public string NumeroContrato { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAlteracao { get; set; }
    public Guid FornecedorId { get; set; }
    public string? FornecedorDescricao { get; set; }
    public decimal Desconto { get; set; }
    public decimal ValorTotal { get; set; }
    public List<ItemContratoDto> Itens { get; set; }
}

public class CreateContratoVendaDto
{
    [Required(ErrorMessage = "Número do contrato é obrigatório.")]
    [StringLength(100, ErrorMessage = "Número do contrato deve ter no máximo 100 caracteres.")]
    public string NumeroContrato { get; set; }

    [Required(ErrorMessage = "ID do Fornecedor é obrigatório.")]
    public Guid FornecedorId { get; set; }

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "Desconto não pode ser negativo.")]
    public decimal Desconto { get; set; }

    [Required(ErrorMessage = "O contrato deve possuir itens.")]
    [MinLength(1, ErrorMessage = "O contrato deve possuir pelo menos um item.")]
    public List<CreateItemContratoDto> Itens { get; set; }
}

public class UpdateContratoVendaDto
{
    [Required(ErrorMessage = "Número do contrato é obrigatório.")]
    [StringLength(100, ErrorMessage = "Número do contrato deve ter no máximo 100 caracteres.")]
    public string NumeroContrato { get; set; }

    [Required(ErrorMessage = "ID do Fornecedor é obrigatório.")]
    public Guid FornecedorId { get; set; }

    [Range(0, (double)decimal.MaxValue, ErrorMessage = "Desconto não pode ser negativo.")]
    public decimal Desconto { get; set; }

    [Required(ErrorMessage = "O contrato deve possuir itens.")]
    [MinLength(1, ErrorMessage = "O contrato deve possuir pelo menos um item.")]
    public List<CreateItemContratoDto> Itens { get; set; }
}