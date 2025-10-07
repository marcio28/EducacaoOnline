
namespace EducacaoOnline.PagamentoEFaturamento.Domain
{
    public class DadosCartao
    {
        public string NomeTitular { get; }
        public string NumeroCartao { get; }
        public string CodigoSeguranca { get; }
        public DateTime DataValidade { get; }

        public DadosCartao(string nomeTitular,
                           string numeroCartao,
                           string codigoSeguranca,
                           DateTime dataValidade)
        {
            NomeTitular = nomeTitular;
            NumeroCartao = numeroCartao;
            CodigoSeguranca = codigoSeguranca;
            DataValidade = dataValidade;
        }
    }
}
