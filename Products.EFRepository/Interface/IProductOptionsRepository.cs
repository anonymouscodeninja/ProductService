using System;
using System.Collections.Generic;
using Products.Entity;

namespace Products.EFRepository
{
    public interface IProductOptionsRepository
    {
        List<ProductOption> List(int productId);

        ProductOption Get(int productId, int optionId);

        int Add(ProductOption Product);

        bool Update(ProductOption Product);

        bool Remove(int productId, int OptionId);
    }
}
