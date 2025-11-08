using Microsoft.AspNetCore.Mvc;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Web.Filters;
using PAW3.Web.Models.ViewModels;

namespace PAW3.Web.Controllers;

[RequireLogin]
public class SupplierController : Controller
{
    private readonly IRestProvider _restProvider;
    private readonly IConfiguration _configuration;
    private readonly string _apiBaseUrl;

    public SupplierController(IRestProvider restProvider, IConfiguration configuration)
    {
        _restProvider = restProvider;
        _configuration = configuration;
        _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7180/api";
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/SupplierApi";
            var response = await _restProvider.GetAsync(endpoint, null);
            var suppliers = JsonProvider.DeserializeSimple<List<SupplierViewModel>>(response) ?? new List<SupplierViewModel>();
            return View(suppliers);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading suppliers: {ex.Message}";
            return View(new List<SupplierViewModel>());
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/SupplierApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var supplier = JsonProvider.DeserializeSimple<SupplierViewModel>(response);
            if (supplier == null)
                return NotFound();
            return View(supplier);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading supplier: {ex.Message}";
            return NotFound();
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SupplierViewModel supplier)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/SupplierApi";
                var json = JsonProvider.Serialize(supplier);
                await _restProvider.PostAsync(endpoint, json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error creating supplier: {ex.Message}");
        }
        return View(supplier);
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/SupplierApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var supplier = JsonProvider.DeserializeSimple<SupplierViewModel>(response);
            if (supplier == null)
                return NotFound();
            return View(supplier);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading supplier: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, SupplierViewModel supplier)
    {
        try
        {
            if (id != supplier.SupplierId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/SupplierApi/{id}";
                var json = JsonProvider.Serialize(supplier);
                await _restProvider.PutAsync(endpoint, id.ToString(), json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error updating supplier: {ex.Message}");
        }
        return View(supplier);
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/SupplierApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var supplier = JsonProvider.DeserializeSimple<SupplierViewModel>(response);
            if (supplier == null)
                return NotFound();
            return View(supplier);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading supplier: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/SupplierApi";
            await _restProvider.DeleteAsync(endpoint, id.ToString());
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error deleting supplier: {ex.Message}";
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}

