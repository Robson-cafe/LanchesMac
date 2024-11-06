using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Lanches")]
    public class Lanche
    {
        [Key]
        public int LancheId { get; set; }

        [StringLength(80, MinimumLength =10, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2}")]
        [Required(ErrorMessage = "O nome do lanche deve ser informado")]
        [Display(Name = "Nome do lanche")]
        public string Nome { get; set; }

        [MinLength(20, ErrorMessage ="Descrição deve ter no mínimo {1} cracteres")]
        [MaxLength(200, ErrorMessage = "Descrição não pode exceder {1} caracteres")]
        [Required(ErrorMessage = "Faça uma descrição curta")]
        [Display(Name = "Descrição do lanche")]
        public string DescricaoCurta { get; set; }

        [MinLength(20, ErrorMessage = "Descrição longa deve ter no mínimo {1} cracteres")]
        [MaxLength(200, ErrorMessage = "Descrição não pode exceder {1} caracteres")]
        [Required(ErrorMessage = "Faça uma descrição longa do lanche")]
        [Display(Name = "Descrição longa do lanche")]
        public string DescricaoLonga { get; set; }

        [Required(ErrorMessage = "Informe o preço do lanche")]
        [Display(Name = "Preço")]
        [Column(TypeName ="decimal(10,2")]
        [Range(1, 999.99, ErrorMessage ="O preço deve estar entre 1 e 999,99")]
        public decimal Preco {  get; set; }

        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1}")]
        [Display(Name = "Caminho imagem normal")]
        public string ImagemUrl { get; set; }

        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1}")]
        [Display(Name = "Caminho imagem miniatura")]
        public string ImagemThumbnailUrl { get; set; }

        [Display(Name = "Preferido?")]
        public bool EhLanchePreferido {  get; set; }

        [Display(Name = "Estoque")]
        public bool EmEstoque {  get; set; }

        public int CategoriaId {  get; set; }
        public virtual Categoria Categoria { get; set; }


    }
}
