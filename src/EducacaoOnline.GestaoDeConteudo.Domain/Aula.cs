using EducacaoOnline.GestaoDeConteudo.Domain.Validators;

namespace EducacaoOnline.GestaoDeConteudo.Domain
{
    public class Aula : Entity
    {
        public Guid IdCurso { get; }
        public string Titulo { get; private set; }
        public string Conteudo { get; private set;  }
 
        public Aula(Guid idCurso, string titulo, string conteudo)
        {
            IdCurso = idCurso;
            Titulo = titulo;
            Conteudo = conteudo;
        }

        public override bool EhValido()
        {
            ValidationResult = new AulaValidator().Validate(this);

            var ehValido = ValidationResult.IsValid;

            return ehValido;
        }
    }
}
