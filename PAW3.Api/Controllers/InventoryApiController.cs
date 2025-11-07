using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using PAW3.Models.Entities;

namespace PAW3.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryApiController(IInventoryBusiness inventoryBusiness) : ControllerBase
{
    // GET: api/InventoryApiController
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Inventory>>> Get()
    {
        var inventories = await inventoryBusiness.GetInventories(id: null);
        return Ok(inventories);
    }

    // GET api/InventoryApiController/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Inventory>> Get(int id)
    {
        var inventories = await inventoryBusiness.GetInventories(id);
        var inventory = inventories.FirstOrDefault();
        if (inventory == null)
            return NotFound();
        return Ok(inventory);
    }

    // POST api/InventoryApiController
    [HttpPost]
    public async Task<ActionResult<bool>> Post([FromBody] Inventory inventory)
    {
        var result = await inventoryBusiness.SaveInventoryAsync(inventory);
        if (result)
            return CreatedAtAction(nameof(Get), new { id = inventory.InventoryId }, inventory);
        return BadRequest();
    }

    // PUT api/InventoryApiController/5
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> Put(int id, [FromBody] Inventory inventory)
    {
        if (id != inventory.InventoryId)
            return BadRequest();
        
        var result = await inventoryBusiness.SaveInventoryAsync(inventory);
        if (result)
            return Ok(result);
        return NotFound();
    }

    // DELETE api/InventoryApiController/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var result = await inventoryBusiness.DeleteInventoryAsync(id);
        if (result)
            return Ok(result);
        return NotFound();
    }
}

