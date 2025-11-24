// APIControl.Application/Mapping/AppProfile.cs
using AutoMapper;
using APIControl.Application.DTOs;
using APIControl.Domain.Entities;

namespace APIControl.Application.Mapping
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            // Product mapping
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>()
                .ForMember(d => d.Id, o => o.MapFrom(_ => Guid.NewGuid()))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(_ => DateTime.UtcNow));
            CreateMap<UpdateProductDto, Product>();

            // User mapping
            CreateMap<User, UserDto>();

            CreateMap<RegisterRequest, User>()
                .ForMember(d => d.Id, o => o.MapFrom(_ => Guid.NewGuid()))
                .ForMember(d => d.PasswordHash, o => o.Ignore())
                .ForMember(d => d.Role, o => o.MapFrom(_ => "User"))
                .ForMember(d => d.RefreshToken, o => o.Ignore())
                .ForMember(d => d.RefreshTokenExpiresAt, o => o.Ignore());

            CreateMap<LoginRequest, User>()
                .ForMember(d => d.PasswordHash, o => o.Ignore());

            // AuthResponse mapping
            CreateMap<User, AuthResponse>()
                .ForMember(d => d.UserId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.Role, o => o.MapFrom(s => s.Role ?? "User"))
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductId))
                .ForMember(d => d.AccessToken, o => o.Ignore())
                .ForMember(d => d.RefreshToken, o => o.MapFrom(s => s.RefreshToken))
                .ForMember(d => d.RefreshTokenExpiresAt, o => o.MapFrom(s => s.RefreshTokenExpiresAt));

            // RefreshResponse mapping
            CreateMap<User, RefreshResponse>()
                .ForMember(d => d.AccessToken, o => o.Ignore())
                .ForMember(d => d.AccessTokenExpiresAt, o => o.Ignore())
                .ForMember(d => d.RefreshToken, o => o.MapFrom(s => s.RefreshToken))
                .ForMember(d => d.RefreshTokenExpiresAt, o => o.MapFrom(s => s.RefreshTokenExpiresAt));
        }
    }
}
