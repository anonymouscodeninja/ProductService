using Xunit;
using Products.EFRepository;
using Products.EFRepository.Dal;
using Microsoft.EntityFrameworkCore;
using ProductService.Controllers;
using System;
using Products.BL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Products.Dto;
using AutoMapper;

namespace Product.Test
{
    public class ProductTests
    {
        private readonly ProductsController _productController;
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IMapper _mapper;

        public ProductTests()
        {
            // Setup InMemory Database for Unit testing

            #region Setup

            ProductsDBContext context = GetContext();
            _mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile<ProductProfileMap>(); });
            _mapper = _mapperConfiguration.CreateMapper();
            SeedDatabase(context);
            ProductsRepository productRepository = new ProductsRepository(context);
            ProductOptionsRepository productOptionsRepository = new ProductOptionsRepository(context);
            _productController = new ProductsController(productRepository, productOptionsRepository, _mapper);

            #endregion

        }

        public ProductsDBContext GetContext()
        {
            var options = new DbContextOptionsBuilder<ProductsDBContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            return new ProductsDBContext(options);
        }

        /// <summary>
        /// Seed data In Memory database.
        /// </summary>
        /// <param name="productRepository">The product repository.</param>
        private void SeedDatabase(ProductsDBContext context)
        {
            // Add products
            context.Products.Add(new Products.Entity.Product { Id = 1, Name = "Samsung Galaxy S20", Description = "Latest smartphone from Samsung", Price = 1599.99M, DeliveryPrice = 9.99M });
            context.Products.Add(new Products.Entity.Product { Id = 2, Name = "Apple iPhone X", Description = "Latest smartphone from Apple", Price = 1999.99M, DeliveryPrice = 19.99M });
            context.Products.Add(new Products.Entity.Product { Id = 3, Name = "Samsung Galaxy S Fold", Description = "foldable smartphone from Samsung", Price = 1599.99M, DeliveryPrice = 9.99M });

            // Add Product Options
            context.ProductOptions.Add(new Products.Entity.ProductOption { Id = 1, ProductId = 1, Name = "128 GB Cosmic Gray", Description = "128 GB" });
            context.ProductOptions.Add(new Products.Entity.ProductOption { Id = 2, ProductId = 1, Name = "256 GB Blood Red", Description = "256 GB" });
            context.ProductOptions.Add(new Products.Entity.ProductOption { Id = 3, ProductId = 1, Name = "512 GB Cherry Pink", Description = "512 GB" });
            context.ProductOptions.Add(new Products.Entity.ProductOption { Id = 4, ProductId = 2, Name = "64 GB Black", Description = "64 GB" });
            context.ProductOptions.Add(new Products.Entity.ProductOption { Id = 5, ProductId = 2, Name = "512 GB Gray", Description = "512 GB Gray" });
            context.ProductOptions.Add(new Products.Entity.ProductOption { Id = 6, ProductId = 3, Name = "128 GB Gray", Description = "128 GB" });
            context.ProductOptions.Add(new Products.Entity.ProductOption { Id = 7, ProductId = 3, Name = "256 GB Red", Description = "256 GB" });
            
            context.SaveChanges();
        }

        #region Product

        [Fact]
        public void Test_Get_All_Products_ReturnsOkResult()
        {
            var response = _productController.Search(string.Empty);
            Assert.IsType<OkObjectResult>(response.Result);

        }

        [Fact]
        public void Test_Search_Products_ByName_ReturnsOkResult()
        {
            //Search for products having name 'Samsung'
            var data = _productController.Search("Samsung").Result as OkObjectResult;
            var products = Assert.IsType<List<ProductDto>>(data.Value);
            Assert.Equal(2, products.Count);

        }

        [Fact]
        public void Test_Search_Products_ReturnsNotFound()
        {
            var notFoundResult = _productController.Search("Google");
            Assert.IsType<NotFoundObjectResult>(notFoundResult.Result);
        }

        [Fact]
        public void Test_Get_Product_by_Id_ReturnsOkResult()
        {
            var response = _productController.Get(1);
            Assert.IsType<OkObjectResult>(response.Result);

            var okResult = response.Result as OkObjectResult;
            var product = Assert.IsType<ProductDto>(okResult.Value);
            Assert.Equal("Samsung Galaxy S20", product.Name);
        }


        [Fact]
        public void Test_Create_new_Product_Returns_Success()
        {
            var response = _productController.Add(new ProductDto { Id= 4, Name = "Nokia Lumia 800", Description = "Nokia Lumia 800", Price = 999.99M, DeliveryPrice = 9.99M });
            Assert.IsType<CreatedResult>(response);
        }

        [Fact]
        public void Test_Update_Product_Returns_Success()
        {
            var response = _productController.Update(1, new ProductDto { Id = 1, Name = "Google Pixel 4", Description = "Best camera phone", Price = 1299.99M, DeliveryPrice = 9.99M });
            Assert.IsType<StatusCodeResult>(response);
            var statusCode = response as StatusCodeResult;
            Assert.Equal(204, statusCode.StatusCode);

            var okResult = _productController.Get(1).Result as OkObjectResult;
            var product = Assert.IsType<ProductDto>(okResult.Value);
            Assert.Equal("Google Pixel 4", product.Name);

        }

        [Fact]
        public void Test_Update_Product_Returns_BadRequest()
        {
            int productId = 1, wrongProductId = 2;
            var response = _productController.Update(wrongProductId, new ProductDto { Id = productId, Name = "Google Pixel 4", Description = "Best camera phone", Price = 1299.99M, DeliveryPrice = 9.99M });
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public void Test_Delete_Product_Returns_Success()
        {
            var response = _productController.Delete(1);
            Assert.IsType<StatusCodeResult>(response);
            var statusCode = response as StatusCodeResult;
            Assert.Equal(204, statusCode.StatusCode);
        }

        [Fact]
        public void Test_Delete_Product_Returns_Failure()
        {
            //trying to delete product which doesn't exist
            var response = _productController.Delete(10);
            Assert.IsType<NotFoundObjectResult>(response);

        }
        #endregion
        #region Product Options


        [Fact]
        public void Test_Get_ProductOptions_ByProduct_ReturnsOkResult()
        {
            var response = _productController.GetProductOptions(1);
            Assert.IsType<OkObjectResult>(response.Result);

        }

        [Fact]
        public void Test_Get_ProductOptions_ByProduct_ReturnsNotFoundResult()
        {
            int productId = 10;
            var response = _productController.GetProductOptions(productId);
            Assert.IsType<NotFoundObjectResult>(response.Result);
        }


        [Fact]
        public void Test_Get_ProductOption_by_OptionId_ReturnsOkResult()
        {
            var response = _productController.GetProductOptionByOptionId(1,3);
            Assert.IsType<OkObjectResult>(response.Result);

            var okResult = response.Result as OkObjectResult;
            var product = Assert.IsType<ProductOptionDto>(okResult.Value);
            Assert.Equal("512 GB Cherry Pink", product.Name);
        }


        [Fact]
        public void Test_Create_new_ProductOption_Returns_Success()
        {
            int productId = 3, optionId = 8;
            var response = _productController.AddProductOption(productId, new ProductOptionDto { Id = optionId, ProductId = productId, Name = "512 GB", Description = "512 GB" });
            Assert.IsType<CreatedResult>(response);
        }

        [Fact]
        public void Test_Create_new_ProductOption_Returns_BadRequest()
        {
            int productId = 4, optionId = 8;
            int wrongProducId = 3;
            var response = _productController.AddProductOption(wrongProducId, new ProductOptionDto { Id = optionId, ProductId = productId, Name = "512 GB", Description = "512 GB" });
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public void Test_Update_ProductOption_Returns_Success()
        {
            int productId = 1;
            int optionId = 3;
            var response = _productController.UpdateProductOption(productId,optionId, new ProductOptionDto {Id = optionId, ProductId = productId, Name = "Motorola Rzor", Description = "Best Moto phone ever" });
            Assert.IsType<StatusCodeResult>(response);
            var statusCode = response as StatusCodeResult;
            Assert.Equal(204, statusCode.StatusCode);

            var okResult = _productController.GetProductOptionByOptionId(productId,optionId).Result as OkObjectResult;
            var product = Assert.IsType<ProductOptionDto>(okResult.Value);
            Assert.Equal("Motorola Rzor", product.Name);

        }

        [Fact]
        public void Test_Delete_ProductOption_Returns_Success()
        {
            int productId = 1, optionId = 3;
            var response = _productController.DeleteProductOption(productId, optionId);
            Assert.IsType<StatusCodeResult>(response);
            var statusCode = response as StatusCodeResult;
            Assert.Equal(204, statusCode.StatusCode);
        }

        [Fact]
        public void Test_Delete_ProductOption_Returns_Failure()
        {
            int productId = 10, optionId = 30;
            //trying to delete product option which doesn't exist
            var response = _productController.DeleteProductOption(productId,optionId);
            Assert.IsType<NotFoundObjectResult>(response);

        }


        #endregion

    }
}
