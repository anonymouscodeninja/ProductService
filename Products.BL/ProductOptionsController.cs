using AutoMapper;
using Products.Dto;
using Products.EFRepository;
using System;
using System.Collections.Generic;

namespace Products.BL
{
    public class ProductOptionsController
    {
        private readonly IProductOptionsRepository _ProductOptionsRepository;
        private readonly IMapper _mapper;
        public ProductOptionsController(IProductOptionsRepository ProductOptionsRepository, IMapper Mapper)
        {
            _ProductOptionsRepository = ProductOptionsRepository;
            _mapper = Mapper;
        }

        public List<ProductOptionDto> List(int productId)
        {
            var list = _ProductOptionsRepository.List(productId);
            return _mapper.Map<List<Entity.ProductOption>, List<ProductOptionDto>>(list);
        }

        public ProductOptionDto Get(int productId, int optionId)
        {
            var product = _ProductOptionsRepository.Get(productId,optionId);
            return _mapper.Map<Entity.ProductOption, ProductOptionDto>(product);
        }

        public int Add(ProductOptionCreateDto newProduct)
        {
            Entity.ProductOption product = _mapper.Map<ProductOptionCreateDto, Entity.ProductOption>(newProduct);
            return _ProductOptionsRepository.Add(product);
        }

        public bool Update(int productId, ProductOptionDto Product)
        {
            Entity.ProductOption product = _mapper.Map<ProductOptionDto, Entity.ProductOption>(Product);
            return _ProductOptionsRepository.Update(product);
        }

        public bool Delete(int productId, int optionId)
        {
            return _ProductOptionsRepository.Remove(productId, optionId);
        }
    }
}
