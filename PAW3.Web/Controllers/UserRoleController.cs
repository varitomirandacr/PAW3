using Microsoft.AspNetCore.Mvc;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Web.Models.ViewModels;

namespace PAW3.Web.Controllers;

public class UserRoleController : Controller
{
    private readonly IRestProvider _restProvider;
    private readonly IConfiguration _configuration;
    private readonly string _apiBaseUrl;

    public UserRoleController(IRestProvider restProvider, IConfiguration configuration)
    {
        _restProvider = restProvider;
        _configuration = configuration;
        _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7180/api";
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/UserRoleApi";
            var response = await _restProvider.GetAsync(endpoint, null);
            var userRoles = JsonProvider.DeserializeSimple<List<UserRoleViewModel>>(response) ?? new List<UserRoleViewModel>();
            return View(userRoles);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading user roles: {ex.Message}";
            return View(new List<UserRoleViewModel>());
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/UserRoleApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var userRole = JsonProvider.DeserializeSimple<UserRoleViewModel>(response);
            if (userRole == null)
                return NotFound();
            return View(userRole);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading user role: {ex.Message}";
            return NotFound();
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserRoleViewModel userRole)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/UserRoleApi";
                var json = JsonProvider.Serialize(userRole);
                await _restProvider.PostAsync(endpoint, json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error creating user role: {ex.Message}");
        }
        return View(userRole);
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/UserRoleApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var userRole = JsonProvider.DeserializeSimple<UserRoleViewModel>(response);
            if (userRole == null)
                return NotFound();
            return View(userRole);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading user role: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UserRoleViewModel userRole)
    {
        try
        {
            if (id != (int?)userRole.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/UserRoleApi/{id}";
                var json = JsonProvider.Serialize(userRole);
                await _restProvider.PutAsync(endpoint, id.ToString(), json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error updating user role: {ex.Message}");
        }
        return View(userRole);
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/UserRoleApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var userRole = JsonProvider.DeserializeSimple<UserRoleViewModel>(response);
            if (userRole == null)
                return NotFound();
            return View(userRole);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading user role: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/UserRoleApi";
            await _restProvider.DeleteAsync(endpoint, id.ToString());
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error deleting user role: {ex.Message}";
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}

