using dotnetCache.Models;

namespace dotnetCache.Service
{
    public class ProductService : IProductService
    {
        private static List<Product> products =
        [
            new() { Id = 1, Name = "Product 1", Price = 19.99m },
            new() { Id = 2, Name = "Product 2", Price = 29.99m },
            new() { Id = 3, Name = "Product 3", Price = 39.99m }
        ];

        public Product GetProductById(int id) => products.FirstOrDefault(x => x.Id == id);

        public IEnumerable<Product> GetProducts() => products;

        public void CreateProduct(Product product) => products.Add(product);
    }
}
