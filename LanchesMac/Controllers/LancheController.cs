using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Linq;

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
                //if (string.Equals("Normal", categoria, StringComparison.OrdinalIgnoreCase)) {
                //    lanches = _lancheRepository.Lanches.Where(l => l.Categoria.NomeCategoria.Equals("Normal")).OrderBy(l => l.Nome);
                //} 
                //else {
                //    lanches = _lancheRepository.Lanches.Where(l => l.Categoria.NomeCategoria.Equals("Natural")).OrderBy(l => l.Nome);
                //}
                lanches = _lancheRepository.Lanches.Where(l => l.Categoria.NomeCategoria.Equals(categoria)).OrderBy(c => c.Nome);
                categoriaAtual = categoria;
            }

            var lanchesViewModel = new LancheListViewModel()
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lanchesViewModel);
        }

        public IActionResult Details(int lancheId) {
            var lanche = _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
            return View(lanche); 
        }

        public ViewResult Search(string searchString) {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString)) {
                lanches = _lancheRepository.Lanches.OrderBy(p => p.LancheId);
                categoriaAtual = "Todos os lanches";
            } 
            else {
                lanches = _lancheRepository.Lanches.Where(p => p.Nome.ToLower().Contains(searchString.ToLower()));

                if (lanches.Any()) {
                    categoriaAtual = "Lanches";
                } 
                else {
                    categoriaAtual = "Nenhum lanche encontrado";
                }
            }

            return View("~/Views/Lanche/List.cshtml", new LancheListViewModel
            {
                Lanches= lanches,
                CategoriaAtual= categoriaAtual
            });
        }        
    }
}
