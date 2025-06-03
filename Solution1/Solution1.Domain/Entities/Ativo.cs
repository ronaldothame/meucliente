namespace Solution1.Domain.Entities;

public class Ativo
{
    public Guid Id { get; set; }
    public string Codigo { get; set; }
    public string Descricao { get; set; }
    public Guid TipoAtivoId { get; set; }
    public decimal PrecoVenda { get; set; }
    
    public virtual TipoAtivo TipoAtivo { get; set; }
    public virtual ICollection<ItemContrato> ItensContrato { get; set; } = new List<ItemContrato>();
}