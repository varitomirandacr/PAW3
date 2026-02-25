using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using PAW3.Data.Foodbankdb.Models;

namespace PAW3.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FoodItemApiController(IFoodItemBusiness foodItemBusiness) : ControllerBase
{
    // GET: api/FoodItem
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FoodItem>>> Get()
    {
        var items = await foodItemBusiness.GetFoodItems(id: null);
        return Ok(items);
    }

    // GET api/FoodItem/5
    [HttpGet("{id}")]
    public async Task<ActionResult<FoodItem>> Get(int id)
    {
        var items = await foodItemBusiness.GetFoodItems(id);
        var foodItem = items.FirstOrDefault();
        if (foodItem == null)
            return NotFound();
        return Ok(foodItem);
    }

    // POST api/FoodItem
    [HttpPost]
    public async Task<ActionResult<bool>> Post([FromBody] FoodItem foodItem)
    {
        var result = await foodItemBusiness.SaveFoodItemAsync(foodItem);
        if (result)
            return CreatedAtAction(nameof(Get), new { id = foodItem.FoodItemId }, foodItem);
        return BadRequest();
    }

    // PUT api/FoodItem/5
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> Put(int id, [FromBody] FoodItem foodItem)
    {
        if (id != foodItem.FoodItemId)
            return BadRequest();
        
        var result = await foodItemBusiness.SaveFoodItemAsync(foodItem);
        if (result)
            return Ok(result);
        return NotFound();
    }

    // DELETE api/CategoryApiController/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var result = await foodItemBusiness.DeleteFoodItemAsync(id);
        if (result)
            return Ok(result);
        return NotFound();
    }
}

