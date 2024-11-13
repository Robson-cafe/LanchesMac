using LanchesMac.Context;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("CarrinhoCompraItens")] //não precisava, fica redundante o ant framwork core core já reconhece como table
    public class CarrinhoCompraItem
    {
        public int CarrinhoCompraItemId { get; set; }
        
        public Lanche Lanche { get; set; }  

        public int Quantidade {  get; set; }

        [StringLength(200, ErrorMessage = "Tamanho máximo 200 caracteres")]
        public string CarrinhoCompraId { get; set; }
        public string CarrinhoId { get; }
    }
}
