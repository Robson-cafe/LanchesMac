﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LanchesMac.Context;
using LanchesMac.Models;
using Microsoft.AspNetCore.Authorization;
using ReflectionIT.Mvc.Paging;
using LanchesMac.ViewModels;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminPedidosController : Controller
    {
        private readonly AppDbContext _context;

        public AdminPedidosController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult PedidoLanches(int? id)
        {
            var pedido = _context.Pedidos
                .Include(pd => pd.PedidosItens)
                .ThenInclude(l => l.Lanche)
                .FirstOrDefault(p => p.PedidoId == id);

            if (pedido == null)
            {
                Response.StatusCode = 404;
                return View("Pedido Not found", id.Value);
            }

            PedidoLancheViewModel pedidoLancheViewModel = new PedidoLancheViewModel()
            {
                Pedido = pedido,
                PedidoDetalhes = pedido.PedidosItens
            };

            return View(pedidoLancheViewModel);
        }
        // GET: Admin/AdminPedidos
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Pedidos.ToListAsync());
        //}

        public async Task<IActionResult> Index(string filter, int pageIndex = 1, string sort = "Nome")
        {
            var results = _context.Pedidos.AsNoTracking().AsQueryable();

            if(!string.IsNullOrEmpty(filter))
            {
                results = results.Where(p => p.Nome.Contains(filter) /*|| p.SobreNome.Contains(filter)*/);
            }
            
            var model = await PagingList.CreateAsync(
                results, 5, pageIndex, sort, "Nome");

            model.RouteValue = new RouteValueDictionary
            {
                { "filter", filter }
            };

            return View(model);
        }

        // GET: Admin/AdminPedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .FirstOrDefaultAsync(m => m.PedidoId == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Admin/AdminPedidos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminPedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PedidoId,Nome,SobreNome,Endereco1,Endereco2,Cep,Estado,Cidade,Telefone,Email,PedidoEnviado,PedidosEntregueEm")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        // GET: Admin/AdminPedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        // POST: Admin/AdminPedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PedidoId,Nome,SobreNome,Endereco1,Endereco2,Cep,Estado,Cidade,Telefone,Email,PedidoEnviado,PedidosEntregueEm")] Pedido pedido)
        {
            if (id != pedido.PedidoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.PedidoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        // GET: Admin/AdminPedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .FirstOrDefaultAsync(m => m.PedidoId == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Admin/AdminPedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.PedidoId == id);
        }
    }
}
