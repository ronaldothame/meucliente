using Solution1.Domain.DTOs;
using Solution1.Domain.Interfaces;

namespace Solution1.Api.Controllers;

public class TipoAtivoController : BaseController<TipoAtivoDto, CreateTipoAtivoDto, UpdateTipoAtivoDto>
{
    public TipoAtivoController(IService<TipoAtivoDto, CreateTipoAtivoDto, UpdateTipoAtivoDto> service)
        : base(service)
    {
    }
}