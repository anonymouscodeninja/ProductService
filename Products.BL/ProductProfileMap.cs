using AutoMapper;
using Products.Dto;

namespace Products.BL
{
    public class ProductProfileMap: Profile
    {
        public ProductProfileMap()
        {
            CreateMap<Entity.Product, ProductCreateDto>().ReverseMap();
            CreateMap<Entity.Product, ProductDto>().ReverseMap();
            CreateMap<Entity.ProductOption, ProductOptionCreateDto>().ReverseMap();
            CreateMap<Entity.ProductOption, ProductOptionDto>().ReverseMap();
        }
    }
}
