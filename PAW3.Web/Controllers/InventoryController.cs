using Microsoft.AspNetCore.Mvc;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Web.Filters;
using PAW3.Web.Models.ViewModels;

namespace PAW3.Web.Controllers;

[RequireLogin]
public class InventoryController : Controller
{
    private readonly IRestProvider _restProvider;
    private readonly IConfiguration _configuration;
    private readonly string _apiBaseUrl;

    public InventoryController(IRestProvider restProvider, IConfiguration configuration)
    {
        _restProvider = restProvider;
        _configuration = configuration;
        _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7180/api";
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/InventoryApi";
            var response = await _restProvider.GetAsync(endpoint, null);
            var inventories = JsonProvider.DeserializeSimple<List<InventoryViewModel>>(response) ?? new List<InventoryViewModel>();
            return View(inventories);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading inventories: {ex.Message}";
            return View(new List<InventoryViewModel>());
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/InventoryApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var inventory = JsonProvider.DeserializeSimple<InventoryViewModel>(response);
            if (inventory == null)
                return NotFound();
            return View(inventory);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading inventory: {ex.Message}";
            return NotFound();
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(InventoryViewModel inventory)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/InventoryApi";
                var json = JsonProvider.Serialize(inventory);
                await _restProvider.PostAsync(endpoint, json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error creating inventory: {ex.Message}");
        }
        return View(inventory);
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/InventoryApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var inventory = JsonProvider.DeserializeSimple<InventoryViewModel>(response);
            if (inventory == null)
                return NotFound();
            return View(inventory);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading inventory: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, InventoryViewModel inventory)
    {
        try
        {
            if (id != inventory.InventoryId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/InventoryApi/{id}";
                var json = JsonProvider.Serialize(inventory);
                await _restProvider.PutAsync(endpoint, id.ToString(), json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error updating inventory: {ex.Message}");
        }
        return View(inventory);
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/InventoryApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var inventory = JsonProvider.DeserializeSimple<InventoryViewModel>(response);
            if (inventory == null)
                return NotFound();
            return View(inventory);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading inventory: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/InventoryApi";
            await _restProvider.DeleteAsync(endpoint, id.ToString());
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error deleting inventory: {ex.Message}";
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}

