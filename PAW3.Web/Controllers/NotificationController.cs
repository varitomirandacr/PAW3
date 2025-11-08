using Microsoft.AspNetCore.Mvc;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Web.Filters;
using PAW3.Web.Models.ViewModels;

namespace PAW3.Web.Controllers;

[RequireLogin]
public class NotificationController : Controller
{
    private readonly IRestProvider _restProvider;
    private readonly IConfiguration _configuration;
    private readonly string _apiBaseUrl;

    public NotificationController(IRestProvider restProvider, IConfiguration configuration)
    {
        _restProvider = restProvider;
        _configuration = configuration;
        _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7180/api";
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/NotificationApi";
            var response = await _restProvider.GetAsync(endpoint, null);
            var notifications = JsonProvider.DeserializeSimple<List<NotificationViewModel>>(response) ?? new List<NotificationViewModel>();
            return View(notifications);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading notifications: {ex.Message}";
            return View(new List<NotificationViewModel>());
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/NotificationApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var notification = JsonProvider.DeserializeSimple<NotificationViewModel>(response);
            if (notification == null)
                return NotFound();
            return View(notification);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading notification: {ex.Message}";
            return NotFound();
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NotificationViewModel notification)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/NotificationApi";
                var json = JsonProvider.Serialize(notification);
                await _restProvider.PostAsync(endpoint, json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error creating notification: {ex.Message}");
        }
        return View(notification);
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/NotificationApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var notification = JsonProvider.DeserializeSimple<NotificationViewModel>(response);
            if (notification == null)
                return NotFound();
            return View(notification);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading notification: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, NotificationViewModel notification)
    {
        try
        {
            if (id != notification.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/NotificationApi/{id}";
                var json = JsonProvider.Serialize(notification);
                await _restProvider.PutAsync(endpoint, id.ToString(), json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error updating notification: {ex.Message}");
        }
        return View(notification);
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/NotificationApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var notification = JsonProvider.DeserializeSimple<NotificationViewModel>(response);
            if (notification == null)
                return NotFound();
            return View(notification);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading notification: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/NotificationApi";
            await _restProvider.DeleteAsync(endpoint, id.ToString());
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error deleting notification: {ex.Message}";
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}

