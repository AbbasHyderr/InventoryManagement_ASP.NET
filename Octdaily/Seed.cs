
using Octdaily.Data;
using Octdaily.Models;

namespace Octdaily
{
    public class Seed
    {
        private readonly DataContext dataContext;

        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            if (!dataContext.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        id = "1",
                        quantity = 10,
                        name = "Sample Product 1",
                        productId = "ABC123",
                        price = 19.99
                    },
                    new Product
                    {
                        id = "2",
                        quantity = 5,
                        name = "Sample Product 2",
                        productId = "DEF456",
                        price = 29.99
                    },
                    // Add more products as needed
                };

                dataContext.Products.AddRange(products);
                dataContext.SaveChanges();
            }
        }
    }
}
