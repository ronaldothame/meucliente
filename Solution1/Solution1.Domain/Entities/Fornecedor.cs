namespace Solution1.Domain.Entities;

public class Fornecedor
{
    public Guid Id { get; set; }
    public string Codigo { get; set; }
    public string Descricao { get; set; }
    public string Cnpj { get; set; }

    public virtual ICollection<ContratoVenda> Contratos { get; set; } = new List<ContratoVenda>();
}