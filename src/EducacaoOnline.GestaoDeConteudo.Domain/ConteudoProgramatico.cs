
namespace EducacaoOnline.GestaoDeConteudo.Domain
{
    public class ConteudoProgramatico(string descricao)
    {
        public string Descricao { get; private set; } = descricao;
    }
}