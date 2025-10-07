using EducacaoOnline.Core.DomainObjects.Messages;
using EducacaoOnline.PagamentoEFaturamento.Domain;
using FluentValidation;

namespace EducacaoOnline.PagamentoEFaturamento.Application
{
    public class RealizarPagamentoCommand : Command
    {
        public Guid IdMatricula { get; }
        public DadosCartao DadosCartao { get; }
        
        public RealizarPagamentoCommand(Guid idMatricula,
                                        DadosCartao dadosCartao)
        {
            IdMatricula = idMatricula;
            DadosCartao = dadosCartao;
        }

        public override bool EhValido()
        {
            ValidationResult = new RealizarPagamentoValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RealizarPagamentoValidator : AbstractValidator<RealizarPagamentoCommand>
    {
        public static string IdMatriculaErroMsg => "Id da matrícula inválido";
        public static string DadosCartaoErroMsg => "Dados do cartão inválido";

        public RealizarPagamentoValidator()
        {
            RuleFor(c => c.IdMatricula)
                .NotEqual(Guid.Empty)
                .WithMessage(IdMatriculaErroMsg);

            RuleFor(c => c.DadosCartao)
                .NotEmpty()
                .WithMessage(DadosCartaoErroMsg);
        }
    }
}
