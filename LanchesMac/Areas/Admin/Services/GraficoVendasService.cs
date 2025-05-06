using LanchesMac.Context;
using LanchesMac.Models;
using System.Data;

namespace LanchesMac.Areas.Admin.Services
{
    public class GraficoVendasService
    {
        private readonly AppDbContext dbContext;

        public GraficoVendasService(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public List<LancheGrafico> GetVendasLanches(int dias = 360) 
        { 
            var data = DateTime.Now.AddDays(-dias);

            var lanches = (from pd in dbContext.PedidosDetalhe
                           join l in dbContext.Lanches on pd.LancheId equals l.LancheId
                           where pd.Pedido.PedidoEnviado >= data
                           group pd by new { pd.LancheId, l.Nome }
                           into g
                           select new
                           {
                                LancheNome = g.Key.Nome,
                                LanchesQuantidade = g.Sum(q => q.Quantidade),
                                LanchesValorTotal = g.Sum(a => a.Preco * a.Quantidade)
                           });

            var lista = new List<LancheGrafico>();

            foreach (var lanche in lanches)
            {
                var novoLanche = new LancheGrafico();
                novoLanche.LancheNome = lanche.LancheNome;
                novoLanche.LancheQuantidade = lanche.LanchesQuantidade;
                novoLanche.LancheValorTotal = lanche.LanchesValorTotal;

                lista.Add(novoLanche);
            }

            return lista;
        }
    }
}
