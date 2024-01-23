using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<CatalogController> _logger;

    public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _productRepository.GetAll();
        return Ok(products);
    }


    [HttpGet("{id:length(24)}", Name = "GetProduct")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> GetById(string id)
    {
        var product = await _productRepository.GetById(id);
        if (product == null)
        {
            _logger.LogError($"Product with id: {id}, not found.");
            return NotFound();
        }
        return Ok(product);
    }


    [Route("[action]/{category}", Name = "GetByCategory")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetByCategory(string category)
    {
        var products = await _productRepository.GetByCategory(category);
        return Ok(products);
    }


    [HttpPost]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> Create([FromBody] Product product)
    {
        await _productRepository.Create(product);

        return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
    }


    [HttpPut]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update([FromBody] Product product)
    {
        return Ok(await _productRepository.Update(product));
    }


    [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteById(string id)
    {
        return Ok(await _productRepository.Delete(id));
    }

}
