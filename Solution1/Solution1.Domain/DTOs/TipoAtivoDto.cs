namespace Solution1.Domain.DTOs;

public class TipoAtivoDto
{
    public Guid Id { get; set; }
    public string Codigo { get; set; }
    public string Descricao { get; set; }
}

public class CreateTipoAtivoDto
{
    public string Codigo { get; set; }
    public string Descricao { get; set; }
}

public class UpdateTipoAtivoDto
{
    public string Codigo { get; set; }
    public string Descricao { get; set; }
}