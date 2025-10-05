﻿using EducacaoOnline.GestaoDeConteudo.Domain.Validators;

namespace EducacaoOnline.GestaoDeConteudo.Domain
{
    public class Aula : Entity
    {
        public Guid IdCurso { get; }
        public string Titulo { get; private set; } = string.Empty;
        public string Conteudo { get; private set; } = string.Empty;
        public string NomeArquivoMaterial { get; private set; } = string.Empty;

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
