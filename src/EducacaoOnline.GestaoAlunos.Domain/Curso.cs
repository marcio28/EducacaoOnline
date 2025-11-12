using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.GestaoAlunos.Domain
{
    public class Curso : Entity
    {
        public bool DisponivelMatricula { get; }

        public Curso(bool disponivelMatricula)
        {
            DisponivelMatricula = disponivelMatricula;
        }
    }
}