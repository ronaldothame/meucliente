using Microsoft.AspNetCore.Mvc;
using Solution1.Domain.DTOs;
using Solution1.Domain.Services;

namespace Solution1.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class ContratoVendaController : ControllerBase
{
    private readonly IContratoVendaService _contratoVendaService;

    public ContratoVendaController(IContratoVendaService contratoVendaService)
    {
        _contratoVendaService = contratoVendaService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ContratoVendaDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ContratoVendaDto>>> GetAll()
    {
        var contratos = await _contratoVendaService.GetAllAsync();
        return Ok(contratos);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ContratoVendaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContratoVendaDto>> GetById(Guid id)
    {
        var contrato = await _contratoVendaService.GetByIdAsync(id);

        if (contrato == null)
            return NotFound($"Contrato de venda com ID {id} não encontrado.");

        return Ok(contrato);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ContratoVendaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContratoVendaDto>> Create([FromBody] CreateContratoVendaDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var contrato = await _contratoVendaService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = contrato.Id },
                contrato);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ContratoVendaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContratoVendaDto>> Update(Guid id, [FromBody] UpdateContratoVendaDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var contrato = await _contratoVendaService.UpdateAsync(id, dto);

            if (contrato == null)
                return NotFound($"Contrato de venda com ID {id} não encontrado.");

            return Ok(contrato);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _contratoVendaService.DeleteAsync(id);

        if (!success)
            return NotFound($"Contrato de venda com ID {id} não encontrado.");

        return NoContent();
    }
}