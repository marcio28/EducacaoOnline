namespace EducacaoOnline.GestaoAlunos.Domain.Services
{
    public interface IAlunoService
    {
        Task Incluir(Aluno aluno, CancellationToken tokenCancelamento);
    }
}