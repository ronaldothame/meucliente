using Solution1.Domain.DTOs;
using Solution1.Domain.Interfaces;

namespace Solution1.Api.Controllers;

public class FornecedorController : BaseController<FornecedorDto, CreateFornecedorDto, UpdateFornecedorDto>
{
    public FornecedorController(IService<FornecedorDto, CreateFornecedorDto, UpdateFornecedorDto> service)
        : base(service)
    {
    }
}