
using FluentValidation.Results;
using System.Collections.ObjectModel;

namespace EducacaoOnline.GestaoDeConteudo.Domain
{
    public class Curso(string nome, 
                       ConteudoProgramatico conteudoProgratico) : Entity
    {
        public string Nome { get; private set; } = nome;
        public ConteudoProgramatico ConteudoProgramatico { get; private set; } = conteudoProgratico;
        public Collection<Aula>? Aulas { get; private set; }

        public override bool EhValido() 
        {
            ValidationResult = new CursoValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}