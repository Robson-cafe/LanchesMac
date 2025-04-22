using LanchesMac.Context;
using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LanchesMac.Areas.Admin.Services
{
    public class RelatorioVendasService
    {
        private readonly AppDbContext dbContext;

        public RelatorioVendasService(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<List<Pedido>> FindByDateAsync (DateTime? minDate, DateTime? maxDate)
        {
            IQueryable<Pedido> resultado = from obj in dbContext.Pedidos select obj;

            if(minDate.HasValue)
                resultado = resultado.Where(x => x.PedidoEnviado >= minDate.Value);

            if (maxDate.HasValue)
                resultado = resultado.Where(x => x.PedidoEnviado <= maxDate.Value);

            return await resultado
                        .Include(l => l.PedidosItens)
                        .ThenInclude(l => l.Lanche)
                        .OrderByDescending(x => x.PedidoEnviado)
                        .ToListAsync();
        }
    }

}
