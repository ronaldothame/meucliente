using Microsoft.AspNetCore.Mvc;
using Solution1.Domain.DTOs;
using Solution1.Domain.Services;

namespace Solution1.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class TipoAtivoController : ControllerBase
{
    private readonly ITipoAtivoService _tipoAtivoService;

    public TipoAtivoController(ITipoAtivoService tipoAtivoService)
    {
        _tipoAtivoService = tipoAtivoService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TipoAtivoDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TipoAtivoDto>>> GetAll()
    {
        var tiposAtivo = await _tipoAtivoService.GetAllAsync();
        return Ok(tiposAtivo);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TipoAtivoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoAtivoDto>> GetById(Guid id)
    {
        var tipoAtivo = await _tipoAtivoService.GetByIdAsync(id);

        if (tipoAtivo == null)
            return NotFound($"Tipo de ativo não encontrado.");

        return Ok(tipoAtivo);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TipoAtivoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoAtivoDto>> Create([FromBody] CreateTipoAtivoDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var tipoAtivo = await _tipoAtivoService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = tipoAtivo.Id },
            tipoAtivo);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(TipoAtivoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoAtivoDto>> Update(Guid id, [FromBody] UpdateTipoAtivoDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var tipoAtivo = await _tipoAtivoService.UpdateAsync(id, dto);

        if (tipoAtivo == null)
            return NotFound($"Tipo de ativo não encontrado.");

        return Ok(tipoAtivo);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _tipoAtivoService.DeleteAsync(id);

        if (!success)
            return NotFound($"Tipo de ativo não encontrado.");

        return NoContent();
    }
}