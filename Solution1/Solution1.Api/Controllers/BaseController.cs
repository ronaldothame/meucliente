using Microsoft.AspNetCore.Mvc;
using Solution1.Domain.Interfaces;

namespace Solution1.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public abstract class BaseController<TDto, TCreateDto, TUpdateDto> : ControllerBase
    where TDto : class, IEntityDto
    where TCreateDto : class
    where TUpdateDto : class
{
    protected readonly IService<TDto, TCreateDto, TUpdateDto> _service;

    protected BaseController(IService<TDto, TCreateDto, TUpdateDto> service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public virtual async Task<ActionResult<IEnumerable<TDto>>> GetAll()
    {
        var entities = await _service.GetAllAsync();
        return Ok(entities);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual async Task<ActionResult<TDto>> GetById(Guid id)
    {
        var entity = await _service.GetByIdAsync(id);
        return Ok(entity);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult<TDto>> Create([FromBody] TCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = entity.Id },
            entity);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult<TDto>> Update(Guid id, [FromBody] TUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = await _service.UpdateAsync(id, dto);
        return Ok(entity);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}