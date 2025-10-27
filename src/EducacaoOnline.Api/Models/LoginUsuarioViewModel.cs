using System.ComponentModel.DataAnnotations;

namespace EducacaoOnline.Api.Models;

public class LoginUsuarioViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo {0} deve ser um endereço de e-mail válido.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 6)]
    public required string Senha { get; set; }
}
