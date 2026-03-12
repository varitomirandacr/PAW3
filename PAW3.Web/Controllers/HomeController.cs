using Microsoft.AspNetCore.Mvc;
using PAW3.Web.Filters;
using PAW3.Web.Models;
using PAW3.Web.Models.ViewModels;
using System.Diagnostics;

namespace PAW3.Web.Controllers
{
    [RequireLogin]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new CursoViewModel
            {
                curso = "Programacion Avanzada Web",
                Sede = "San José",
                aula = "Remoto",
                facultadAsociada = "Facultad de Ingeniería",
                correo = "sistemas@ufidelitas.ac.cr"
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
