using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using PAW3.Models.Entities.Productdb;

namespace PAW3.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserActionApiController(IUserActionBusiness userActionBusiness) : ControllerBase
{
    // GET: api/UserActionApiController
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserAction>>> Get()
    {
        var userActions = await userActionBusiness.GetUserActions(id: null);
        return Ok(userActions);
    }

    // GET api/UserActionApiController/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserAction>> Get(int id)
    {
        var userActions = await userActionBusiness.GetUserActions(id);
        var userAction = userActions.FirstOrDefault();
        if (userAction == null)
            return NotFound();
        return Ok(userAction);
    }

    // POST api/UserActionApiController
    [HttpPost]
    public async Task<ActionResult<bool>> Post([FromBody] UserAction userAction)
    {
        var result = await userActionBusiness.SaveUserActionAsync(userAction);
        if (result)
            return CreatedAtAction(nameof(Get), new { id = userAction.Id }, userAction);
        return BadRequest();
    }

    // PUT api/UserActionApiController/5
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> Put(int id, [FromBody] UserAction userAction)
    {
        if (id != (int?)userAction.Id)
            return BadRequest();
        
        var result = await userActionBusiness.SaveUserActionAsync(userAction);
        if (result)
            return Ok(result);
        return NotFound();
    }

    // DELETE api/UserActionApiController/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var result = await userActionBusiness.DeleteUserActionAsync(id);
        if (result)
            return Ok(result);
        return NotFound();
    }
}

