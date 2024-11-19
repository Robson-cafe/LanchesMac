using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List(string categoria)
        {
            //var lanches = _lancheRepository.Lanches;          
            //return View(lanches);

            //var lanchesViewModel = new LancheListViewModel();
            //lanchesViewModel.Lanches = _lancheRepository.Lanches;
            //lanchesViewModel.Categoria = "Categoria atual";
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria)) {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os lanches";
            } 
            else {
                if (string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase)) {
                    lanches = _lancheRepository.Lanches.Where(l => l.Categoria.NomeCategoria.Equals("Normal")).OrderBy(l => l.Nome);
                } 
                else {
                    lanches = _lancheRepository.Lanches.Where(l => l.Categoria.NomeCategoria.Equals("Natural")).OrderBy(l => l.Nome);
                }

                categoriaAtual = categoria;
            }

            var lanchesViewModel = new LancheListViewModel()
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lanchesViewModel);
        }
    }
}
