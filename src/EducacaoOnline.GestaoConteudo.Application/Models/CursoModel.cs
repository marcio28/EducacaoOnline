using EducacaoOnline.GestaoConteudo.Domain.Validators;
using System.ComponentModel.DataAnnotations;

namespace EducacaoOnline.GestaoConteudo.Application.Models
{
    public class CursoModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(CursoValidator.TamanhoNomeMaximo, 
            ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = CursoValidator.TamanhoNomeMinimo)]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(CursoValidator.TamanhoConteudoMaximo, 
            ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = CursoValidator.TamanhoConteudoMinimo)]
        public required string Descricao { get; set; }
    }
}