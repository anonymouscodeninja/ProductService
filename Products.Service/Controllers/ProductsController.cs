using Products.BL;
using Products.Dto;
using Products.EFRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using AutoMapper;

namespace ProductService.Controllers
{
    /// <summary>
    /// Product Managament API with CRUD Operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductController _ProductController;
        private readonly ProductOptionsController _ProductOptionsController;
        public ProductsController(IProductsRepository productRepository, IProductOptionsRepository productOptionsRepository, IMapper mapper)
        {
            _ProductController = new ProductController(productRepository, mapper);
            _ProductOptionsController = new ProductOptionsController(productOptionsRepository, mapper);
        }

        #region Products

        /// <summary>
        /// gets all products and/or applies search criteria e.g. search by Name
        /// </summary>
        /// <param name="name">Optional field. Returns data by a Name filter.</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(500)]
        [HttpGet]
        public ActionResult<List<ProductDto>> Search(string name)
        {
            try
            {
                var data = _ProductController.Search(name);
                if (data?.Count > 0)
                    return Ok(data);
                else
                    return NotFound("No Products found for the search criteria");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// gets the project that matches the specified ID.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(500)]
        [HttpGet("{id}")]
        public ActionResult<ProductDto> Get(int id)
        {
            try
            {
                var Product = _ProductController.Get(id);
                if (Product != null)
                    return Ok(Product);
                else
                    return NotFound("Product could not be found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        /// <summary>
        /// creates a new product.
        /// </summary>
        /// <param name="product">Product details</param>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost]
        public ActionResult Add([FromBody] ProductCreateDto product)
        {
            try
            {
                var id = _ProductController.Add(product);
                if (id > 0)
                {
                    return Created(string.Format($"/api/Products/{id}", id), "Product has been created");
                }
                else
                    return BadRequest("There is problem with request");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// updates a product.
        /// </summary>
        /// <param name="id">Product Id which needs to be updated</param>
        /// <param name="product">Product details</param>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] ProductDto product)
        {
            try
            {
                if (id != product.Id)
                    return BadRequest("Unable to process the request due to invalid input");

                if (_ProductController.Update(product))
                    return StatusCode(StatusCodes.Status204NoContent);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, "Update failed");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// deletes a product and its options
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns></returns>
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var response = _ProductController.Delete(id);
                if (response)
                    return StatusCode(StatusCodes.Status204NoContent);
                else
                    return NotFound("The Product resource doesn't exist");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        #endregion


        #region Product Options

        /// <summary>
        /// finds all options for a specified product.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(500)]
        [HttpGet("{id}/options")]
        public ActionResult<List<ProductOptionDto>> GetProductOptions(int id)
        {
            try
            {
                var productOption = _ProductOptionsController.List(id);
                if (productOption != null && productOption.Count > 0)
                    return Ok(productOption);
                else
                    return NotFound("Product Options could not be found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        /// <summary>
        /// finds the specified product option for the specified product.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="optionId">Option Id</param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(500)]
        [HttpGet("{id}/options/{optionId}")]
        public ActionResult<ProductOptionDto> GetProductOptionByOptionId(int id, int optionId)
        {
            try
            {
                var productOption = _ProductOptionsController.Get(id, optionId);
                if (productOption != null)
                    return Ok(productOption);
                else
                    return NotFound("Product Options could not be found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        /// <summary>
        ///  adds a new product option to the specified product.
        /// </summary>
        /// <param name="Product">Product option details</param>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost("{id}/options")]
        public ActionResult AddProductOption(int id, [FromBody] ProductOptionCreateDto productOption)
        {
            try
            {
                if (id != productOption.ProductId)
                    return BadRequest("Unable to process the request due to invalid input");

                var optionId = _ProductOptionsController.Add(productOption);
                if (optionId > 0)
                {
                    return Created(string.Format($"/api/Products/{id}/options/{optionId}"), "Product option has been created");
                }
                else
                    return BadRequest("There is problem with request");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        ///  updates the specified product option.
        /// </summary>
        /// <param name="id">Product Id which needs to be updated</param>
        /// <param name="Product">Product details</param>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPut("{id}/options/{optionId}")]
        public ActionResult UpdateProductOption(int id, int optionId, [FromBody] ProductOptionDto productOption)
        {
            try
            {
                // verify the route values and body inputs
                if (id != productOption.ProductId || optionId != productOption.Id)
                    return BadRequest("Unable to process the request due to invalid input");

                if (_ProductOptionsController.Update(id, productOption))
                    return StatusCode(StatusCodes.Status204NoContent);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, "Update failed");
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// deletes the specified product option.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns></returns>
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [HttpDelete("{id}/options/{optionId}")]
        public ActionResult DeleteProductOption(int id, int optionId)
        {
            try
            {
                var response = _ProductOptionsController.Delete(id, optionId);
                if (response)
                    return StatusCode(StatusCodes.Status204NoContent);
                else
                    return NotFound("The Product option resource doesn't exist");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        #endregion
    }
}
