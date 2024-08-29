
using dotnetCache.Models;
using dotnetCache.Service;
using Microsoft.AspNetCore.Mvc;

namespace dotnetCache.Controllers;


[ApiController]
[Route("[controller]")]
public class ProductController(ICacheService cacheService, IProductService productService) : ControllerBase
{
    private readonly ICacheService _cacheService = cacheService;
    private readonly IProductService _productService = productService;

    [HttpGet]
    public IActionResult GetProduct()
    {
        var key = "productList";
        var cachedProduct = _cacheService.Get(key);

        if (cachedProduct != null)
        {
            return Ok(cachedProduct);
        }

        var productList = _productService.GetProducts();

        _cacheService.Set(key, productList);

        return Ok(productList);
    }

    [HttpGet("{id}")]
    public IActionResult GetProductById(int id)
    {
        var cachedProduct = _cacheService.Get(id.ToString());

        if (cachedProduct != null)
        {
            return Ok(cachedProduct);
        }

        var productList = _productService.GetProductById(id);

        _cacheService.Set(id.ToString(), productList);

        return Ok(productList);
    }

    [HttpPost]
    public IActionResult SetProduct([FromBody] Product product)
    {
        _productService.CreateProduct(product);
        _cacheService.Set(product.Id.ToString(), product);
        return Ok($"Produto com chave '{product.Id}' adicionado ao cache.");
    }

  
}
