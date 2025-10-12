using EducacaoOnline.Core.DomainObjects;

namespace EducacaoOnline.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}