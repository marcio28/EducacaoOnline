using EducacaoOnline.Core.DomainObjects.Messages;
using EducacaoOnline.PagamentoEFaturamento.Application.Validators;
using EducacaoOnline.PagamentoEFaturamento.Domain;
using FluentValidation.Results;

namespace EducacaoOnline.PagamentoEFaturamento.Application
{
    public class RealizarPagamentoCommand : Command
    {
        public Guid IdMatricula { get; }
        public DadosCartao DadosCartao { get; }
        public List<ValidationFailure> ErrosCartao => DadosCartao.ValidationResult?.Errors ?? [];

        public RealizarPagamentoCommand(Guid idMatricula,
                                        DadosCartao dadosCartao)
        {
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
