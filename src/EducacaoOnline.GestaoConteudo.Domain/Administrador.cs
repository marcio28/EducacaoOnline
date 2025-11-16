using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.GestaoConteudo.Domain
{
    public class Administrador : Entity, IAggregateRoot 
    {
        protected Administrador() { }

        public Administrador(Guid id) : base(id) { }   
    }
}