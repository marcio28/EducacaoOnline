
using EducacaoOnline.GestaoDeConteudo.Domain.Exceptions;
using EducacaoOnline.GestaoDeConteudo.Domain.Validators;
using System.Collections.ObjectModel;

namespace EducacaoOnline.GestaoDeConteudo.Domain
{
    public class Curso(string nome, 
                       ConteudoProgramatico conteudoProgratico) : Entity
    {
        public string Nome { get; private set; } = nome;
        public ConteudoProgramatico ConteudoProgramatico { get; private set; } = conteudoProgratico;
        public bool DisponivelParaMatricula { get; private set; } = true;
        public Collection<Aula>? Aulas { get; private set; }

        public override bool EhValido() 
        {
            ValidationResult = new CursoValidator().Validate(this);

            var ehValido = ValidationResult.IsValid;
            if (!ehValido)
                TornarIndisponivelParaMatricula();

            return ehValido;
        }

        public void TornarDisponivelParaMatricula()
        {
            if (!EhValido())
                throw new DisponibilizacaoDeCursoInvalidoException();
        }

        public void TornarIndisponivelParaMatricula()
        {
            DisponivelParaMatricula = false;
        }
    }
}