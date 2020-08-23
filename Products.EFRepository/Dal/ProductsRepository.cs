using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Products.EFRepository.Dal
{
    public class ProductsRepository : IProductsRepository
    {
        private ProductsDBContext _context;
        public ProductsRepository(ProductsDBContext context)
        {
            _context = context;
        }

        public List<Entity.Product> Search(string searchText)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchText))
                {
                    return _context.Set<Entity.Product>().Where(BuildLikeExpression(searchText)).ToList();
                }
                else
                {
                    return _context.Products.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Entity.Product Get(int id)
        {
            try
            {
                return _context.Products.FirstOrDefault(x => x.Id == id);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int Add(Entity.Product Product)
        {
            try
            {
                _context.Products.Add(Product);
                _context.SaveChanges();
                return Product.Id;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(Entity.Product Product)
        {
            try
            {
                var prod = _context.Products.FirstOrDefault(x => x.Id == Product.Id);
                if (prod != null)
                {
                    prod.Name = Product.Name;
                    prod.Description = Product.Description;
                    prod.Price = Product.Price;
                    prod.DeliveryPrice = Product.DeliveryPrice;

                    _context.Products.Update(prod);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                var prod = _context.Products.FirstOrDefault(x => x.Id == id);
                var productOptions = _context.ProductOptions.Where(x => x.ProductId == id);
                if (prod != null)
                {
                    _context.Products.Remove(prod);
                    // Remove product options associated with product
                    if (productOptions != null)
                    {
                        _context.ProductOptions.RemoveRange(productOptions);
                    }
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected Expression<Func<Entity.Product, bool>> BuildLikeExpression(string searchText)
        {
            var likeSearch = $"%{searchText}%";
            return t => EF.Functions.Like(t.Name, likeSearch);
        }
    }
}
