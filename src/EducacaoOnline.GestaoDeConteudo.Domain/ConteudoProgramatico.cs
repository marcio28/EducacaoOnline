namespace EducacaoOnline.GestaoDeConteudo.Domain
{
    public class ConteudoProgramatico(string descricao)
    {
        public string Descricao { get; private set; } = descricao;
      
        public bool EhValido()
        {
            // TODO: Refatorar para usar FluentValidation
            return true;
        }
    }
}