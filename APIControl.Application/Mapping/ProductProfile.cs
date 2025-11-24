// APIControl.Application/Mapping/ProductProfile.cs
using AutoMapper;
using APIControl.Application.DTOs;
using APIControl.Domain.Entities;

namespace APIControl.Application.Mapping
{

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>()
                .ForMember(d => d.Id, o => o.MapFrom(_ => Guid.NewGuid()))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(_ => DateTime.UtcNow));
            CreateMap<UpdateProductDto, Product>();
        }
    }
}
