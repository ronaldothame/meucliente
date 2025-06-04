using Solution1.Domain.DTOs;
using Solution1.Domain.Interfaces;

namespace Solution1.Api.Controllers;

public class ContratoVendaController : BaseController<ContratoVendaDto, CreateContratoVendaDto, UpdateContratoVendaDto>
{
    public ContratoVendaController(IService<ContratoVendaDto, CreateContratoVendaDto, UpdateContratoVendaDto> service)
        : base(service)
    {
    }
}