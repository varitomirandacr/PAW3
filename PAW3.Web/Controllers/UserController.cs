using Microsoft.AspNetCore.Mvc;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Web.Filters;
using PAW3.Web.Models.ViewModels;

namespace PAW3.Web.Controllers;

[RequireLogin]
public class UserController : Controller
{
    private readonly IRestProvider _restProvider;
    private readonly IConfiguration _configuration;
    private readonly string _apiBaseUrl;

    public UserController(IRestProvider restProvider, IConfiguration configuration)
    {
        _restProvider = restProvider;
        _configuration = configuration;
        _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7180/api";
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/UserApi";
            var response = await _restProvider.GetAsync(endpoint, null);
            var users = JsonProvider.DeserializeSimple<List<UserViewModel>>(response) ?? new List<UserViewModel>();
            return View(users);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading users: {ex.Message}";
            return View(new List<UserViewModel>());
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/UserApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var user = JsonProvider.DeserializeSimple<UserViewModel>(response);
            if (user == null)
                return NotFound();
            return View(user);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading user: {ex.Message}";
            return NotFound();
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserViewModel user)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/UserApi";
                var json = JsonProvider.Serialize(user);
                await _restProvider.PostAsync(endpoint, json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error creating user: {ex.Message}");
        }
        return View(user);
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/UserApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var user = JsonProvider.DeserializeSimple<UserViewModel>(response);
            if (user == null)
                return NotFound();
            return View(user);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading user: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UserViewModel user)
    {
        try
        {
            if (id != user.UserId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/UserApi/{id}";
                var json = JsonProvider.Serialize(user);
                await _restProvider.PutAsync(endpoint, id.ToString(), json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error updating user: {ex.Message}");
        }
        return View(user);
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/UserApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var user = JsonProvider.DeserializeSimple<UserViewModel>(response);
            if (user == null)
                return NotFound();
            return View(user);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading user: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/UserApi";
            await _restProvider.DeleteAsync(endpoint, id.ToString());
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error deleting user: {ex.Message}";
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}

