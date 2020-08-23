using AutoMapper;
using Products.Dto;
using Products.EFRepository;
using System.Collections.Generic;

namespace Products.BL
{
    public class ProductController
    {
        private readonly IProductsRepository _ProductRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductsRepository ProductRepository, IMapper mapper)
        {
            _ProductRepository = ProductRepository;
            _mapper = mapper;
        }

        public List<ProductDto> Search(string searchName = "")
        {
            var list = _ProductRepository.Search(searchName);
            return _mapper.Map<List<Entity.Product>, List<ProductDto>>(list);
        }

        public ProductDto Get(int id)
        {
            var product = _ProductRepository.Get(id);
            return _mapper.Map<Entity.Product, ProductDto>(product);
        }

        public int Add(ProductCreateDto newProduct)
        {
            Entity.Product product = _mapper.Map<ProductCreateDto, Entity.Product>(newProduct);
            return _ProductRepository.Add(product);
        }

        public bool Update(ProductDto Product)
        {
            Entity.Product product = _mapper.Map<ProductDto, Entity.Product>(Product);
            return _ProductRepository.Update(product);
        }

        public bool Delete(int ProductId)
        {
            return _ProductRepository.Remove(ProductId);
        }
    }
}
