using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using PAW3.Models.Entities.Productdb;

namespace PAW3.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationApiController(INotificationBusiness notificationBusiness) : ControllerBase
{
    // GET: api/NotificationApiController
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Notification>>> Get()
    {
        var notifications = await notificationBusiness.GetNotifications(id: null);
        return Ok(notifications);
    }

    // GET api/NotificationApiController/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Notification>> Get(int id)
    {
        var notifications = await notificationBusiness.GetNotifications(id);
        var notification = notifications.FirstOrDefault();
        if (notification == null)
            return NotFound();
        return Ok(notification);
    }

    // POST api/NotificationApiController
    [HttpPost]
    public async Task<ActionResult<bool>> Post([FromBody] Notification notification)
    {
        var result = await notificationBusiness.SaveNotificationAsync(notification);
        if (result)
            return CreatedAtAction(nameof(Get), new { id = notification.Id }, notification);
        return BadRequest();
    }

    // PUT api/NotificationApiController/5
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> Put(int id, [FromBody] Notification notification)
    {
        if (id != notification.Id)
            return BadRequest();
        
        var result = await notificationBusiness.SaveNotificationAsync(notification);
        if (result)
            return Ok(result);
        return NotFound();
    }

    // DELETE api/NotificationApiController/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var result = await notificationBusiness.DeleteNotificationAsync(id);
        if (result)
            return Ok(result);
        return NotFound();
    }
}

