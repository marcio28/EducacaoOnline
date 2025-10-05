
using EducacaoOnline.Core.DomainObjects;
using EducacaoOnline.GestaoDeConteudo.Domain.Exceptions;
using EducacaoOnline.GestaoDeConteudo.Domain.Validators;
using System.Collections.ObjectModel;

namespace EducacaoOnline.GestaoDeConteudo.Domain
{
    public class Curso(string nome, 
                       ConteudoProgramatico conteudoProgramatico) : Entity
    {
        public string Nome { get; private set; } = nome;
        public ConteudoProgramatico ConteudoProgramatico { get; private set; } = conteudoProgramatico;
        public bool DisponivelMatricula { get; private set; } = false;
        public Collection<Aula>? Aulas { get; private set; } = [];

        public override bool EhValido() 
        {
            ValidationResult = new CursoValidator().Validate(this);

            var ehValido = ValidationResult.IsValid;
            if (!ehValido)
                TornarIndisponivelMatricula();

            return ehValido;
        }

        public void DisponibilizarMatricula()
        {
            if (!EhValido())
                throw new DisponibilizacaoCursoInvalidoException();

            DisponivelMatricula = true;
        }

        public void TornarIndisponivelMatricula()
        {
            DisponivelMatricula = false;
        }

        public void AdicionarAula(string titulo, string conteudo)
        {
            var aula = new Aula(idCurso: this.Id,
                                titulo: titulo,
                                conteudo: conteudo,
                                nomeArquivoMaterial: default);
            if (!aula.EhValido()) throw new DomainException(message: "Aula inválida",
                                                            validationFailures: aula.ValidationResult?.Errors);

            Aulas!.Add(aula);
        }
    }
}