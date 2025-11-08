using Microsoft.AspNetCore.Mvc;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Web.Filters;
using PAW3.Web.Models.ViewModels;
using System.Text.Json;

namespace PAW3.Web.Controllers;

[RequireLogin]
public class CategoryController : Controller
{
    private readonly IRestProvider _restProvider;
    private readonly IConfiguration _configuration;
    private readonly string _apiBaseUrl;

    public CategoryController(IRestProvider restProvider, IConfiguration configuration)
    {
        _restProvider = restProvider;
        _configuration = configuration;
        _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7180/api";
    }

    // GET: Category
    public async Task<IActionResult> Index()
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/CategoryApi";
            var response = await _restProvider.GetAsync(endpoint, null);
            var categories = JsonProvider.DeserializeSimple<List<CategoryViewModel>>(response) ?? new List<CategoryViewModel>();
            return View(categories);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading categories: {ex.Message}";
            return View(new List<CategoryViewModel>());
        }
    }

    // GET: Category/Details/5
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/CategoryApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var category = JsonProvider.DeserializeSimple<CategoryViewModel>(response);
            if (category == null)
                return NotFound();
            return View(category);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading category: {ex.Message}";
            return NotFound();
        }
    }

    // GET: Category/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Category/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryViewModel category)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/CategoryApi";
                var json = JsonProvider.Serialize(category);
                await _restProvider.PostAsync(endpoint, json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error creating category: {ex.Message}");
        }
        return View(category);
    }

    // GET: Category/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/CategoryApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var category = JsonProvider.DeserializeSimple<CategoryViewModel>(response);
            if (category == null)
                return NotFound();
            return View(category);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading category: {ex.Message}";
            return NotFound();
        }
    }

    // POST: Category/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CategoryViewModel category)
    {
        try
        {
            if (id != category.CategoryId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/CategoryApi/{id}";
                var json = JsonProvider.Serialize(category);
                await _restProvider.PutAsync(endpoint, id.ToString(), json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error updating category: {ex.Message}");
        }
        return View(category);
    }

    // GET: Category/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/CategoryApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var category = JsonProvider.DeserializeSimple<CategoryViewModel>(response);
            if (category == null)
                return NotFound();
            return View(category);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading category: {ex.Message}";
            return NotFound();
        }
    }

    // POST: Category/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/CategoryApi";
            await _restProvider.DeleteAsync(endpoint, id.ToString());
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error deleting category: {ex.Message}";
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}

