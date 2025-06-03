namespace Solution1.Domain.DTOs;

public class AtivoDto
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
    public string Codigo { get; set; }
    public string Descricao { get; set; }
    public Guid TipoAtivoId { get; set; }
    public decimal PrecoVenda { get; set; }
}

public class UpdateAtivoDto
{
    public string Codigo { get; set; }
    public string Descricao { get; set; }
    public Guid TipoAtivoId { get; set; }
    public decimal PrecoVenda { get; set; }
}