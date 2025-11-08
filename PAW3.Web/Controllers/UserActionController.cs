using Microsoft.AspNetCore.Mvc;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Web.Filters;
using PAW3.Web.Models.ViewModels;

namespace PAW3.Web.Controllers;

[RequireLogin]
public class UserActionController : Controller
{
    private readonly IRestProvider _restProvider;
    private readonly IConfiguration _configuration;
    private readonly string _apiBaseUrl;

    public UserActionController(IRestProvider restProvider, IConfiguration configuration)
    {
        _restProvider = restProvider;
        _configuration = configuration;
        _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7180/api";
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/UserActionApi";
            var response = await _restProvider.GetAsync(endpoint, null);
            var userActions = JsonProvider.DeserializeSimple<List<UserActionViewModel>>(response) ?? new List<UserActionViewModel>();
            return View(userActions);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading user actions: {ex.Message}";
            return View(new List<UserActionViewModel>());
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/UserActionApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var userAction = JsonProvider.DeserializeSimple<UserActionViewModel>(response);
            if (userAction == null)
                return NotFound();
            return View(userAction);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading user action: {ex.Message}";
            return NotFound();
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserActionViewModel userAction)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/UserActionApi";
                var json = JsonProvider.Serialize(userAction);
                await _restProvider.PostAsync(endpoint, json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error creating user action: {ex.Message}");
        }
        return View(userAction);
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/UserActionApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var userAction = JsonProvider.DeserializeSimple<UserActionViewModel>(response);
            if (userAction == null)
                return NotFound();
            return View(userAction);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading user action: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UserActionViewModel userAction)
    {
        try
        {
            if (id != (int?)userAction.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/UserActionApi/{id}";
                var json = JsonProvider.Serialize(userAction);
                await _restProvider.PutAsync(endpoint, id.ToString(), json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error updating user action: {ex.Message}");
        }
        return View(userAction);
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/UserActionApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var userAction = JsonProvider.DeserializeSimple<UserActionViewModel>(response);
            if (userAction == null)
                return NotFound();
            return View(userAction);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading user action: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/UserActionApi";
            await _restProvider.DeleteAsync(endpoint, id.ToString());
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error deleting user action: {ex.Message}";
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}

