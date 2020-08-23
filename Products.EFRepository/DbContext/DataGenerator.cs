using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Products.EFRepository
{
    public class DataGenerator
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ProductsDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<ProductsDBContext>>()))
            {
                // Look for any Products already in database.
                if (context.Products.Any())
                {
                    return;   // Database has been seeded
                }

                //context.Products.AddRange(
                //    new Entity.Product
                //    {
                //        Id = 1,
                //        FirstName = "John",
                //        LastName = "Doe",
                //        DateOfBirth = new DateTime(1988,12,12)
                //    },
                //    new Entity.Product
                //    {
                //        Id = 2,
                //        FirstName = "John",
                //        LastName = "Lee",
                //        DateOfBirth = new DateTime(1989, 12, 02)
                //    },
                //    new Entity.Product
                //    {
                //        Id = 3,
                //        FirstName = "Jake",
                //        LastName = "Peralta",
                //        DateOfBirth = new DateTime(1985, 12, 09)
                //    },
                //    new Entity.Product
                //    {
                //        Id = 4,
                //        FirstName = "Jane",
                //        LastName = "Doe",
                //        DateOfBirth = new DateTime(1965, 12, 06)
                //    },
                //    new Entity.Product
                //    {
                //        Id = 5,
                //        FirstName = "Brad",
                //        LastName = "Pitt",
                //        DateOfBirth = new DateTime(1967, 01, 01)
                //    },
                //    new Entity.Product
                //    {
                //        Id = 6,
                //        FirstName = "Sam",
                //        LastName = "Diaz",
                //        DateOfBirth = new DateTime(2000, 12, 05)
                //    }
                //    );

                context.SaveChanges();
            }
        }
    }
}
