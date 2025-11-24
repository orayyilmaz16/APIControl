// APIControl.Application/Services/ProductService.cs
using AutoMapper;
using APIControl.Application.DTOs;
using APIControl.Domain.Abstractions;
using APIControl.Domain.Entities;

namespace APIControl.Application.Services
{

    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork uow, IMapper mapper) { _uow = uow; _mapper = mapper; }

        public async Task<IReadOnlyList<ProductDto>> ListAsync()
        {
            var products = await _uow.Repository<Product>().GetAsync();
            return _mapper.Map<IReadOnlyList<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetAsync(Guid id)
        {
            var p = await _uow.Repository<Product>().GetByIdAsync(id);
            return p is null ? null : _mapper.Map<ProductDto>(p);
        }

        public async Task<Guid> CreateAsync(CreateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            await _uow.Repository<Product>().AddAsync(entity);
            await _uow.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateProductDto dto)
        {
            var repo = _uow.Repository<Product>();
            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return false;
            _mapper.Map(dto, existing);
            repo.Update(existing);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var repo = _uow.Repository<Product>();
            var existing = await repo.GetByIdAsync(id);
            if (existing is null) return false;
            repo.Remove(existing);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
