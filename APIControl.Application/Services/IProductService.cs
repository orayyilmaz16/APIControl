// APIControl.Application/Services/IProductService.cs
using APIControl.Application.DTOs;

namespace APIControl.Application.Services;

public interface IProductService
{
    Task<IReadOnlyList<ProductDto>> ListAsync();
    Task<ProductDto?> GetAsync(Guid id);
    Task<Guid> CreateAsync(CreateProductDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateProductDto dto);
    Task<bool> DeleteAsync(Guid id);
}
