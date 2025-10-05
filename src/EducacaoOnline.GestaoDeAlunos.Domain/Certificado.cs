
using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.GestaoDeAlunos.Domain
{
    public class Certificado : Entity
    {
        Guid IdAluno { get; }
        Guid IdCurso { get; }
    }
}
