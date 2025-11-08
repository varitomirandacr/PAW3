using Microsoft.AspNetCore.Mvc;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Web.Filters;
using PAW3.Web.Models.ViewModels;

namespace PAW3.Web.Controllers;

[RequireLogin]
public class ComponentController : Controller
{
    private readonly IRestProvider _restProvider;
    private readonly IConfiguration _configuration;
    private readonly string _apiBaseUrl;

    public ComponentController(IRestProvider restProvider, IConfiguration configuration)
    {
        _restProvider = restProvider;
        _configuration = configuration;
        _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7180/api";
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/ComponentApi";
            var response = await _restProvider.GetAsync(endpoint, null);
            var components = JsonProvider.DeserializeSimple<List<ComponentViewModel>>(response) ?? new List<ComponentViewModel>();
            return View(components);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading components: {ex.Message}";
            return View(new List<ComponentViewModel>());
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/ComponentApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var component = JsonProvider.DeserializeSimple<ComponentViewModel>(response);
            if (component == null)
                return NotFound();
            return View(component);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading component: {ex.Message}";
            return NotFound();
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ComponentViewModel component)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/ComponentApi";
                var json = JsonProvider.Serialize(component);
                await _restProvider.PostAsync(endpoint, json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error creating component: {ex.Message}");
        }
        return View(component);
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/ComponentApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var component = JsonProvider.DeserializeSimple<ComponentViewModel>(response);
            if (component == null)
                return NotFound();
            return View(component);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading component: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ComponentViewModel component)
    {
        try
        {
            if (id != (int)component.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/ComponentApi/{id}";
                var json = JsonProvider.Serialize(component);
                await _restProvider.PutAsync(endpoint, id.ToString(), json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error updating component: {ex.Message}");
        }
        return View(component);
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/ComponentApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var component = JsonProvider.DeserializeSimple<ComponentViewModel>(response);
            if (component == null)
                return NotFound();
            return View(component);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading component: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/ComponentApi";
            await _restProvider.DeleteAsync(endpoint, id.ToString());
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error deleting component: {ex.Message}";
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}

