using System.Collections.Generic;
using Products.Entity;

namespace Products.EFRepository
{
    public interface IProductsRepository
    {
        List<Product> Search(string searchText);

        Product Get(int id);

        int Add(Product Product);

        bool Update(Product Product);

        bool Remove(int id);
    }
}
