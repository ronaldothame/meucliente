namespace Solution1.Domain.DTOs;

public class FornecedorDto
{
    public Guid Id { get; set; }
    public string Codigo { get; set; }
    public string Descricao { get; set; }
    public string Cnpj { get; set; }
}

public class CreateFornecedorDto
{
    public string Codigo { get; set; }
    public string Descricao { get; set; }
    public string Cnpj { get; set; }
}

public class UpdateFornecedorDto
{
    public string Codigo { get; set; }
    public string Descricao { get; set; }
    public string Cnpj { get; set; }
}