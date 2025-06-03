namespace Solution1.Domain.Entities;

public class ContratoVenda
{
    public Guid Id { get; set; }
    public string NumeroContrato { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAlteracao { get; set; }
    public Guid FornecedorId { get; set; }
    public decimal Desconto { get; set; }
    public decimal ValorTotal { get; set; }
    
    public virtual Fornecedor Fornecedor { get; set; }
    public virtual ICollection<ItemContrato> Itens { get; set; } = new List<ItemContrato>();
}