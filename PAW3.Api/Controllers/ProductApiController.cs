using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using PAW3.Models.DTO;
using PAW3.Models.Entities.Productdb;

namespace PAW3.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductApiController(IProductBusiness productBusiness) : ControllerBase
{
    // GET: api/ProductApi
    [HttpGet]
    public async Task<ActionResult<ProductDTO>> Get()
    {
        var products = await productBusiness.GetProducts(id: null);
        return Ok(products);
    }

    // GET api/ProductApiController/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> Get(int id)
    {
        var products = await productBusiness.GetProducts(id);
        var product = products.Products.FirstOrDefault();
        if (product == null)
            return NotFound();
        return Ok(product);
    }

    // POST api/ProductApiController
    [HttpPost]
    public async Task<ActionResult<bool>> Post([FromBody] Product product)
    {
        var result = await productBusiness.SaveProductAsync(product);
        if (result)
            return CreatedAtAction(nameof(Get), new { id = product.ProductId }, product);
        return BadRequest();
    }

    // PUT api/ProductApiController/5
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> Put(int id, [FromBody] Product product)
    {
        if (id != product.ProductId)
            return BadRequest();
        
        var result = await productBusiness.SaveProductAsync(product);
        if (result)
            return Ok(result);
        return NotFound();
    }

    // DELETE api/ProductApiController/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var result = await productBusiness.DeleteProductAsync(id);
        if (result)
            return Ok(result);
        return NotFound();
    }
}
