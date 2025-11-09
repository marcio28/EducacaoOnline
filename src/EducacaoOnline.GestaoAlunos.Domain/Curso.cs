
using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.GestaoDeAlunos.Domain
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