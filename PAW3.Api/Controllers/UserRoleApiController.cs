using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using PAW3.Models.Entities;

namespace PAW3.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserRoleApiController(IUserRoleBusiness userRoleBusiness) : ControllerBase
{
    // GET: api/UserRoleApiController
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserRole>>> Get()
    {
        var userRoles = await userRoleBusiness.GetUserRoles(id: null);
        return Ok(userRoles);
    }

    // GET api/UserRoleApiController/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserRole>> Get(int id)
    {
        var userRoles = await userRoleBusiness.GetUserRoles(id);
        var userRole = userRoles.FirstOrDefault();
        if (userRole == null)
            return NotFound();
        return Ok(userRole);
    }

    // POST api/UserRoleApiController
    [HttpPost]
    public async Task<ActionResult<bool>> Post([FromBody] UserRole userRole)
    {
        var result = await userRoleBusiness.SaveUserRoleAsync(userRole);
        if (result)
            return CreatedAtAction(nameof(Get), new { id = userRole.Id }, userRole);
        return BadRequest();
    }

    // PUT api/UserRoleApiController/5
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> Put(int id, [FromBody] UserRole userRole)
    {
        if (id != (int?)userRole.Id)
            return BadRequest();
        
        var result = await userRoleBusiness.SaveUserRoleAsync(userRole);
        if (result)
            return Ok(result);
        return NotFound();
    }

    // DELETE api/UserRoleApiController/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var result = await userRoleBusiness.DeleteUserRoleAsync(id);
        if (result)
            return Ok(result);
        return NotFound();
    }
}

