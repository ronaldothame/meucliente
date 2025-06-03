using Microsoft.AspNetCore.Mvc;
using Solution1.Domain.DTOs;
using Solution1.Domain.Services;

namespace Solution1.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class AtivoController : ControllerBase
{
    private readonly IAtivoService _ativoService;

    public AtivoController(IAtivoService ativoService)
    {
        _ativoService = ativoService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AtivoDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AtivoDto>>> GetAll()
    {
        var ativos = await _ativoService.GetAllAsync();
        return Ok(ativos);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AtivoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AtivoDto>> GetById(Guid id)
    {
        var ativo = await _ativoService.GetByIdAsync(id);

        if (ativo == null)
            return NotFound($"Ativo não encontrado.");

        return Ok(ativo);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AtivoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AtivoDto>> Create([FromBody] CreateAtivoDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var ativo = await _ativoService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = ativo.Id },
            ativo);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(AtivoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AtivoDto>> Update(Guid id, [FromBody] UpdateAtivoDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var ativo = await _ativoService.UpdateAsync(id, dto);

        if (ativo == null)
            return NotFound($"Ativo não encontrado.");

        return Ok(ativo);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _ativoService.DeleteAsync(id);

        if (!success)
            return NotFound($"Ativo não encontrado.");

        return NoContent();
    }
}