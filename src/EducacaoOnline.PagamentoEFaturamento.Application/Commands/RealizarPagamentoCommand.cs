using EducacaoOnline.Core.Messages;
using EducacaoOnline.PagamentoEFaturamento.Application.Validators;
using EducacaoOnline.PagamentoEFaturamento.Domain;
using FluentValidation.Results;

namespace EducacaoOnline.PagamentoEFaturamento.Application.Commands
{
    public class RealizarPagamentoCommand : Command
    {
        public Matricula Matricula { get; }
        public Guid IdMatricula => Matricula.Id;
        public StatusMatricula StatusMatricula => Matricula.Status;
        public DadosCartao DadosCartao { get; }
        public List<ValidationFailure> ErrosCartao => DadosCartao.ValidationResult?.Errors ?? [];

        public RealizarPagamentoCommand(Matricula matricula,
                                        DadosCartao dadosCartao)
        {
            Matricula = matricula;
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