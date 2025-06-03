namespace Solution1.Domain.Entities;

public class ItemContrato
{
    public Guid Id { get; set; }
    public Guid ContratoVendaId { get; set; }
    public Guid AtivoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    
    public virtual ContratoVenda ContratoVenda { get; set; }
    public virtual Ativo Ativo { get; set; }
}