using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using PAW3.Models.Entities;

namespace PAW3.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComponentApiController(IComponentBusiness componentBusiness) : ControllerBase
{
    // GET: api/ComponentApiController
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Component>>> Get()
    {
        var components = await componentBusiness.GetComponents(id: null);
        return Ok(components);
    }

    // GET api/ComponentApiController/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Component>> Get(int id)
    {
        var components = await componentBusiness.GetComponents(id);
        var component = components.FirstOrDefault();
        if (component == null)
            return NotFound();
        return Ok(component);
    }

    // POST api/ComponentApiController
    [HttpPost]
    public async Task<ActionResult<bool>> Post([FromBody] Component component)
    {
        var result = await componentBusiness.SaveComponentAsync(component);
        if (result)
            return CreatedAtAction(nameof(Get), new { id = component.Id }, component);
        return BadRequest();
    }

    // PUT api/ComponentApiController/5
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> Put(int id, [FromBody] Component component)
    {
        if (id != (int)component.Id)
            return BadRequest();
        
        var result = await componentBusiness.SaveComponentAsync(component);
        if (result)
            return Ok(result);
        return NotFound();
    }

    // DELETE api/ComponentApiController/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var result = await componentBusiness.DeleteComponentAsync(id);
        if (result)
            return Ok(result);
        return NotFound();
    }
}

