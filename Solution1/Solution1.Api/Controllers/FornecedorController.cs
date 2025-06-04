using Microsoft.AspNetCore.Mvc;
using Solution1.Domain.DTOs;
using Solution1.Domain.Interfaces;

namespace Solution1.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class FornecedorController : ControllerBase
{
    private readonly IService<FornecedorDto, CreateFornecedorDto, UpdateFornecedorDto> _fornecedorService;

    public FornecedorController(IService<FornecedorDto, CreateFornecedorDto, UpdateFornecedorDto> fornecedorService)
    {
        _fornecedorService = fornecedorService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FornecedorDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<FornecedorDto>>> GetAll()
    {
        var fornecedores = await _fornecedorService.GetAllAsync();
        return Ok(fornecedores);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(FornecedorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FornecedorDto>> GetById(Guid id)
    {
        var fornecedor = await _fornecedorService.GetByIdAsync(id);

        if (fornecedor == null)
            return NotFound($"Fornecedor não encontrado.");

        return Ok(fornecedor);
    }

    [HttpPost]
    [ProducesResponseType(typeof(FornecedorDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FornecedorDto>> Create([FromBody] CreateFornecedorDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var fornecedor = await _fornecedorService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = fornecedor.Id },
            fornecedor);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(FornecedorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FornecedorDto>> Update(Guid id, [FromBody] UpdateFornecedorDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var fornecedor = await _fornecedorService.UpdateAsync(id, dto);

        if (fornecedor == null)
            return NotFound($"Fornecedor não encontrado.");

        return Ok(fornecedor);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _fornecedorService.DeleteAsync(id);

        if (!success)
            return NotFound($"Fornecedor não encontrado.");

        return NoContent();
    }
}