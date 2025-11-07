using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using PAW3.Models.Entities;

namespace PAW3.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryApiController(ICategoryBusiness categoryBusiness) : ControllerBase
{
    // GET: api/CategoryApiController
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> Get()
    {
        var categories = await categoryBusiness.GetCategories(id: null);
        return Ok(categories);
    }

    // GET api/CategoryApiController/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> Get(int id)
    {
        var categories = await categoryBusiness.GetCategories(id);
        var category = categories.FirstOrDefault();
        if (category == null)
            return NotFound();
        return Ok(category);
    }

    // POST api/CategoryApiController
    [HttpPost]
    public async Task<ActionResult<bool>> Post([FromBody] Category category)
    {
        var result = await categoryBusiness.SaveCategoryAsync(category);
        if (result)
            return CreatedAtAction(nameof(Get), new { id = category.CategoryId }, category);
        return BadRequest();
    }

    // PUT api/CategoryApiController/5
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> Put(int id, [FromBody] Category category)
    {
        if (id != category.CategoryId)
            return BadRequest();
        
        var result = await categoryBusiness.SaveCategoryAsync(category);
        if (result)
            return Ok(result);
        return NotFound();
    }

    // DELETE api/CategoryApiController/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var result = await categoryBusiness.DeleteCategoryAsync(id);
        if (result)
            return Ok(result);
        return NotFound();
    }
}

