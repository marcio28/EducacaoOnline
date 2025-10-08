
using EducacaoOnline.Core.DomainObjects;
using EducacaoOnline.GestaoDeConteudo.Domain.Exceptions;
using EducacaoOnline.GestaoDeConteudo.Domain.Validators;
using FluentValidation.Results;
using System.Collections.ObjectModel;

namespace EducacaoOnline.GestaoDeConteudo.Domain
{
    public class Curso : Entity
    {
        public string Nome { get; private set; }
        public ConteudoProgramatico ConteudoProgramatico { get; private set; }
        public bool DisponivelMatricula { get; private set; }
        public Collection<Aula>? Aulas { get; private set; }
        public int QuantidadeAulas => Aulas?.Count ?? 0;

        public Curso(string nome,
                     ConteudoProgramatico conteudoProgramatico)
        {
            Nome = nome;
            ConteudoProgramatico = conteudoProgramatico;
            DisponivelMatricula = false;
            Aulas = [];
        }

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

        public Aula AdicionarAula(string titulo, string conteudo)
        {
            var aula = new Aula(idCurso: this.Id,
                                titulo: titulo,
                                conteudo: conteudo,
                                nomeArquivoMaterial: default);

            if (aula.EhValido())
            {
                Aulas ??= [];
                Aulas.Add(aula);
            }

            return aula;
        }
    }
}