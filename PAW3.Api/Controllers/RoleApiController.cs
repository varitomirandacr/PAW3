using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using PAW3.Models.Entities;

namespace PAW3.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleApiController(IRoleBusiness roleBusiness) : ControllerBase
{
    // GET: api/RoleApiController
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Role>>> Get()
    {
        var roles = await roleBusiness.GetRoles(id: null);
        return Ok(roles);
    }

    // GET api/RoleApiController/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Role>> Get(int id)
    {
        var roles = await roleBusiness.GetRoles(id);
        var role = roles.FirstOrDefault();
        if (role == null)
            return NotFound();
        return Ok(role);
    }

    // POST api/RoleApiController
    [HttpPost]
    public async Task<ActionResult<bool>> Post([FromBody] Role role)
    {
        var result = await roleBusiness.SaveRoleAsync(role);
        if (result)
            return CreatedAtAction(nameof(Get), new { id = role.RoleId }, role);
        return BadRequest();
    }

    // PUT api/RoleApiController/5
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> Put(int id, [FromBody] Role role)
    {
        if (id != role.RoleId)
            return BadRequest();
        
        var result = await roleBusiness.SaveRoleAsync(role);
        if (result)
            return Ok(result);
        return NotFound();
    }

    // DELETE api/RoleApiController/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var result = await roleBusiness.DeleteRoleAsync(id);
        if (result)
            return Ok(result);
        return NotFound();
    }
}

