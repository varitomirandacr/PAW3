using Microsoft.AspNetCore.Mvc;
using PAW3.Architecture;
using PAW3.Architecture.Providers;
using PAW3.Web.Models.ViewModels;

namespace PAW3.Web.Controllers;

public class TaskController : Controller
{
    private readonly IRestProvider _restProvider;
    private readonly IConfiguration _configuration;
    private readonly string _apiBaseUrl;

    public TaskController(IRestProvider restProvider, IConfiguration configuration)
    {
        _restProvider = restProvider;
        _configuration = configuration;
        _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7180/api";
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/TaskApi";
            var response = await _restProvider.GetAsync(endpoint, null);
            var tasks = JsonProvider.DeserializeSimple<List<TaskViewModel>>(response) ?? new List<TaskViewModel>();
            return View(tasks);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading tasks: {ex.Message}";
            return View(new List<TaskViewModel>());
        }
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/TaskApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var task = JsonProvider.DeserializeSimple<TaskViewModel>(response);
            if (task == null)
                return NotFound();
            return View(task);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading task: {ex.Message}";
            return NotFound();
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TaskViewModel task)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/TaskApi";
                var json = JsonProvider.Serialize(task);
                await _restProvider.PostAsync(endpoint, json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error creating task: {ex.Message}");
        }
        return View(task);
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/TaskApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var task = JsonProvider.DeserializeSimple<TaskViewModel>(response);
            if (task == null)
                return NotFound();
            return View(task);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading task: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TaskViewModel task)
    {
        try
        {
            if (id != task.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var endpoint = $"{_apiBaseUrl}/TaskApi/{id}";
                var json = JsonProvider.Serialize(task);
                await _restProvider.PutAsync(endpoint, id.ToString(), json);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error updating task: {ex.Message}");
        }
        return View(task);
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/TaskApi/{id}";
            var response = await _restProvider.GetAsync(endpoint, id.ToString());
            var task = JsonProvider.DeserializeSimple<TaskViewModel>(response);
            if (task == null)
                return NotFound();
            return View(task);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error loading task: {ex.Message}";
            return NotFound();
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var endpoint = $"{_apiBaseUrl}/TaskApi";
            await _restProvider.DeleteAsync(endpoint, id.ToString());
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Error deleting task: {ex.Message}";
            return RedirectToAction(nameof(Delete), new { id });
        }
    }
}

