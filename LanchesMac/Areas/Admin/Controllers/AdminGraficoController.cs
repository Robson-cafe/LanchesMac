using LanchesMac.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminGraficoController : Controller
    {
        private readonly GraficoVendasService graficoVendas;

        public AdminGraficoController(GraficoVendasService _graficoVendas)
        {
            graficoVendas = _graficoVendas ?? throw new ArgumentNullException(nameof(_graficoVendas));
        }


        public JsonResult VendasLanches(int dias)
        {
            var lanchesVendasTotais = graficoVendas.GetVendasLanches(dias);
            return Json(lanchesVendasTotais);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VendasMensal()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VendasSemanal()
        {
            return View();
        }
    }
}
