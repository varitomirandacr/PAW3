using Microsoft.AspNetCore.Mvc;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Web.Models.ViewModels;

namespace PAW3.Web.Controllers;

public class ProductController : Controller
{
    private readonly IRestProvider _restProvider;
    private readonly IConfiguration _configuration;
    private readonly string _apiBaseUrl;

    public ProductController(IRestProvider restProvider, IConfiguration configuration)
    {
        _restProvider = restProvider;
        _configuration = configuration;
        _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7180/api";
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/ProductApi";
            var response = await _restProvider.GetAsync(endpoint, null);
            var productDto = JsonProvider.DeserializeSimple<ProductDtoViewModel>(response);
            if (productDto == null)
            {
                productDto = new ProductDtoViewModel();
            }
            return View(productDto);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading products: {ex.Message}";
            return View(new ProductDtoViewModel());
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/ProductApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var product = JsonProvider.DeserializeSimple<ProductViewModel>(response);
            if (product == null)
                return NotFound();
            return View(product);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading product: {ex.Message}";
            return NotFound();
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductViewModel product)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/ProductApi";
                var json = JsonProvider.Serialize(product);
                await _restProvider.PostAsync(endpoint, json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error creating product: {ex.Message}");
        }
        return View(product);
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/ProductApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var product = JsonProvider.DeserializeSimple<ProductViewModel>(response);
            if (product == null)
                return NotFound();
            return View(product);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading product: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductViewModel product)
    {
        try
        {
            if (id != product.ProductId)
                return NotFound();

            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/ProductApi/{id}";
                var json = JsonProvider.Serialize(product);
                await _restProvider.PutAsync(endpoint, id.ToString(), json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error updating product: {ex.Message}");
        }
        return View(product);
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/ProductApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var product = JsonProvider.DeserializeSimple<ProductViewModel>(response);
            if (product == null)
                return NotFound();
            return View(product);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading product: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/ProductApi";
            await _restProvider.DeleteAsync(endpoint, id.ToString());
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error deleting product: {ex.Message}";
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}

