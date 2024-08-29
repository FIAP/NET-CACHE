using dotnetCache.Models;

namespace dotnetCache.Service;

public interface IProductService
{
    IEnumerable<Product> GetProducts();
    Product GetProductById(int id);

    void CreateProduct(Product product);
}
