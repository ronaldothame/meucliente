namespace Solution1.Domain.Entities
{
    public class TipoAtivo
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        
        public virtual ICollection<Ativo> Ativos { get; set; } = new List<Ativo>();
    }
}