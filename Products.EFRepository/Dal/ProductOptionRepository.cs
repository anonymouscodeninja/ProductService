using System;
using System.Collections.Generic;
using System.Linq;
using Products.Entity;

namespace Products.EFRepository.Dal
{
    public class ProductOptionsRepository : IProductOptionsRepository
    {
        private ProductsDBContext _context;
        public ProductOptionsRepository(ProductsDBContext context)
        {
            _context = context;
        }


        public List<ProductOption> List(int productId)
        {
            try
            {
                return _context.ProductOptions.Where(x => x.ProductId == productId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductOption Get(int productId, int optionId)
        {
            try
            {
                return _context.ProductOptions.FirstOrDefault(x => x.Id == optionId && x.ProductId == productId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Add(ProductOption ProductOption)
        {
            try
            {
                _context.ProductOptions.Add(ProductOption);
                _context.SaveChanges();

                return ProductOption.Id;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(ProductOption productOption)
        {
            try
            {
                var prod = _context.ProductOptions.FirstOrDefault(x => x.Id == productOption.Id && x.ProductId == productOption.ProductId);
                if (prod != null)
                {
                    prod.Name = productOption.Name;
                    prod.Description = productOption.Description;

                    _context.ProductOptions.Update(prod);
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

        public bool Remove(int productId, int OptionId)
        {
            try
            {
                var prod = _context.ProductOptions.FirstOrDefault(x => x.Id == OptionId && x.ProductId == productId);
                if (prod != null)
                {
                    _context.ProductOptions.Remove(prod);
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
    }
}
