using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using ModelsTask = PAW3.Models.Entities.Productdb.Task;

namespace PAW3.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskApiController(ITaskBusiness taskBusiness) : ControllerBase
{
    // GET: api/TaskApiController
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ModelsTask>>> Get()
    {
        var tasks = await taskBusiness.GetTasks(id: null);
        return Ok(tasks);
    }

    // GET api/TaskApiController/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ModelsTask>> Get(int id)
    {
        var tasks = await taskBusiness.GetTasks(id);
        var task = tasks.FirstOrDefault();
        if (task == null)
            return NotFound();
        return Ok(task);
    }

    // POST api/TaskApiController
    [HttpPost]
    public async Task<ActionResult<bool>> Post([FromBody] ModelsTask task)
    {
        var result = await taskBusiness.SaveTaskAsync(task);
        if (result)
            return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
        return BadRequest();
    }

    // PUT api/TaskApiController/5
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> Put(int id, [FromBody] ModelsTask task)
    {
        if (id != task.Id)
            return BadRequest();
        
        var result = await taskBusiness.SaveTaskAsync(task);
        if (result)
            return Ok(result);
        return NotFound();
    }

    // DELETE api/TaskApiController/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var result = await taskBusiness.DeleteTaskAsync(id);
        if (result)
            return Ok(result);
        return NotFound();
    }
}

