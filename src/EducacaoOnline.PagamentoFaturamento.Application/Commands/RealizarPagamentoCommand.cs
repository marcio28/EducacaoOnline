using EducacaoOnline.Core.Messages;
using EducacaoOnline.PagamentoEFaturamento.Application.Validators;
using EducacaoOnline.PagamentoEFaturamento.Domain;
using FluentValidation.Results;

namespace EducacaoOnline.PagamentoEFaturamento.Application.Commands
{
    public class RealizarPagamentoCommand : Command
    {
        public Guid IdAluno;
        public Guid IdMatricula;
        public DadosCartao DadosCartao { get; }
        public List<ValidationFailure> ErrosCartao => DadosCartao.ValidationResult?.Errors ?? [];

        public RealizarPagamentoCommand(Guid idAluno, Guid idMatricula, DadosCartao dadosCartao)
        {
            IdAluno = idAluno;
            IdMatricula = idMatricula;
            DadosCartao = dadosCartao;
        }

        public override bool EhValido()
        {
            _ = DadosCartao.EhValido();
            ValidationResult = new RealizarPagamentoValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}