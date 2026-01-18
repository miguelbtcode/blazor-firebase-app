using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetFirebase.Api.Authentication;
using NetFirebase.Api.Models.Domain;
using NetFirebase.Api.Services.Products;

namespace NetFirebase.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HasPermission(Permission.ReadMember)]
    [HttpPost]
    public async Task<ActionResult> CreateProduct([FromBody] Product request)
    {
        await _productService.CreateProductAsync(request);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetProductById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        return Ok(product);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult> GetProductByName(string name)
    {
        var product = await _productService.GetProductByNameAsync(name);
        return Ok(product);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateProduct([FromBody] Product request)
    {
        await _productService.UpdateProductAsync(request);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteProductAsync(id);
        return Ok();
    }
}
