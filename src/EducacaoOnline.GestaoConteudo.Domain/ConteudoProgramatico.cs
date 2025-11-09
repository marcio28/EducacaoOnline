
namespace EducacaoOnline.GestaoConteudo.Domain
{
    public class ConteudoProgramatico(string descricao)
    {
        public string Descricao { get; private set; } = descricao;
    }
}