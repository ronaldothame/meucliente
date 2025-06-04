namespace Solution1.Domain.Interfaces;

public interface IService<TDto, in TCreateDto, in TUpdateDto>
    where TDto : class
    where TCreateDto : class
    where TUpdateDto : class
{
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto> GetByIdAsync(Guid id);
    Task<TDto> CreateAsync(TCreateDto dto);
    Task<TDto> UpdateAsync(Guid id, TUpdateDto dto);
    Task DeleteAsync(Guid id);
}