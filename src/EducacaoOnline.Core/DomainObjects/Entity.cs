using EducacaoOnline.Core.Messages;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducacaoOnline.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; }

        [NotMapped]
        List<Event>? _notificacoes;

        public IReadOnlyCollection<Event> Notificacoes => (_notificacoes ?? []).AsReadOnly();

        [NotMapped]
        public ValidationResult? ValidationResult { get; protected set; }

        [NotMapped]
        public List<ValidationFailure> Erros => ValidationResult?.Errors ?? [];

        public int QuantidadeErros => Erros.Count;

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public void AdicionarEvento(Event evento)
        {
            _notificacoes = _notificacoes ?? [];
            _notificacoes.Add(evento);
        }

        public void RemoverEvento(Event evento)
        {
            _notificacoes?.Remove(evento);
        }

        public void LimparEventos()
        {
            _notificacoes?.Clear();
        }

        public override bool Equals(object? obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}