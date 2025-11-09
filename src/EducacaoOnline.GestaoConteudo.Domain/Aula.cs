using EducacaoOnline.Core.DomainObjects;
using EducacaoOnline.GestaoConteudo.Domain.Validators;

namespace EducacaoOnline.GestaoConteudo.Domain
{
    public class Aula : Entity
    {
        public Guid IdCurso { get; }
        public Curso? Curso { get; private set; }
        public string Titulo { get; private set; } = string.Empty;
        public string Conteudo { get; private set; } = string.Empty;
        public string NomeArquivoMaterial { get; private set; } = string.Empty;

        public Aula() { }

        public Aula(Guid idCurso, string titulo, string conteudo, string? nomeArquivoMaterial)
        {
            IdCurso = idCurso;
            Titulo = titulo;
            Conteudo = conteudo;
            NomeArquivoMaterial = nomeArquivoMaterial ?? string.Empty;
        }

        public override bool EhValido()
        {
            ValidationResult = new AulaValidator().Validate(this);

            var ehValido = ValidationResult.IsValid;

            return ehValido;
        }
    }
}
