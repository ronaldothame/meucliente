using Solution1.Domain.DTOs;
using Solution1.Domain.Interfaces;

namespace Solution1.Api.Controllers;

public class AtivoController : BaseController<AtivoDto, CreateAtivoDto, UpdateAtivoDto>
{
    public AtivoController(IService<AtivoDto, CreateAtivoDto, UpdateAtivoDto> service)
        : base(service)
    {
    }
}