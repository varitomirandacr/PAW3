using Microsoft.AspNetCore.Mvc;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Web.Filters;
using PAW3.Web.Models.ViewModels;

namespace PAW3.Web.Controllers;

[RequireLogin]
public class RoleController : Controller
{
    private readonly IRestProvider _restProvider;
    private readonly IConfiguration _configuration;
    private readonly string _apiBaseUrl;

    public RoleController(IRestProvider restProvider, IConfiguration configuration)
    {
        _restProvider = restProvider;
        _configuration = configuration;
        _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7180/api";
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/RoleApi";
            var response = await _restProvider.GetAsync(endpoint, null);
            var roles = JsonProvider.DeserializeSimple<List<RoleViewModel>>(response) ?? new List<RoleViewModel>();
            return View(roles);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading roles: {ex.Message}";
            return View(new List<RoleViewModel>());
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/RoleApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var role = JsonProvider.DeserializeSimple<RoleViewModel>(response);
            if (role == null)
                return NotFound();
            return View(role);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading role: {ex.Message}";
            return NotFound();
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RoleViewModel role)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/RoleApi";
                var json = JsonProvider.Serialize(role);
                await _restProvider.PostAsync(endpoint, json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error creating role: {ex.Message}");
        }
        return View(role);
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/RoleApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var role = JsonProvider.DeserializeSimple<RoleViewModel>(response);
            if (role == null)
                return NotFound();
            return View(role);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading role: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, RoleViewModel role)
    {
        try
        {
            if (id != role.RoleId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/RoleApi/{id}";
                var json = JsonProvider.Serialize(role);
                await _restProvider.PutAsync(endpoint, id.ToString(), json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error updating role: {ex.Message}");
        }
        return View(role);
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/RoleApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var role = JsonProvider.DeserializeSimple<RoleViewModel>(response);
            if (role == null)
                return NotFound();
            return View(role);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading role: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/RoleApi";
            await _restProvider.DeleteAsync(endpoint, id.ToString());
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error deleting role: {ex.Message}";
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}

