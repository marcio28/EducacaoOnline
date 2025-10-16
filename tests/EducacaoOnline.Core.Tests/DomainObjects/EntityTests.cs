using EducacaoOnline.Core.DomainObjects;
using EducacaoOnline.Core.Messages;
using FluentValidation;

namespace EducacaoOnline.Core.Tests.DomainObjects
{
    public class EntityTests
    {
        private readonly EntidadeFake _entidadeFakeValida;
        private readonly EntidadeFake _entidadeFakeInvalida;

        public EntityTests()
        {
            _entidadeFakeValida = new EntidadeFake(1);
            _entidadeFakeInvalida = new EntidadeFake(0);
        }

        [Fact(DisplayName = "Adicionar Evento")]
        [Trait("Categoria", "Core Domain Objects - Entity")]
        public void AdicionarEvento_Evento_DeveConstarNaLista()
        {
            // Arrange
            var eventoFake = new FakeEvent();

            // Act
            _entidadeFakeValida.AdicionarEvento(eventoFake);

            // Assert
            Assert.Contains(eventoFake, _entidadeFakeValida.Notificacoes);
        }

        [Fact(DisplayName = "Remover Evento")]
        [Trait("Categoria", "Core Domain Objects - Entity")]
        public void RemoverEvento_Evento_NaoDeveConstarNaLista()
        {
            // Arrange
            var eventoFake = new FakeEvent();
            _entidadeFakeValida.AdicionarEvento(eventoFake);

            // Act
            _entidadeFakeValida.RemoverEvento(eventoFake);

            // Assert
            Assert.DoesNotContain(eventoFake, _entidadeFakeValida.Notificacoes);
        }

        [Fact(DisplayName = "Limpar Eventos")]
        [Trait("Categoria", "Core Domain Objects - Entity")]
        public void LimparEventos_Eventos_NaoDeveConstarNaLista()
        {
            // Arrange
            var eventoFake1 = new FakeEvent();
            var eventoFake2 = new FakeEvent();
            _entidadeFakeValida.AdicionarEvento(eventoFake1);
            _entidadeFakeValida.AdicionarEvento(eventoFake2);

            // Act
            _entidadeFakeValida.LimparEventos();

            // Assert
            Assert.Empty(_entidadeFakeValida.Notificacoes);
        }

        [Fact(DisplayName = "Comparar Entidades Iguais")]
        [Trait("Categoria", "Core Domain Objects - Entity")]
        public void CompararEntidades_Iguais_DeveRetornarFalse()
        {
            // Arrange feito no construtor

            // Act
            var saoIguais = _entidadeFakeValida == _entidadeFakeInvalida;

            // Assert
            Assert.False(saoIguais);
        }

        [Fact(DisplayName = "Comparar Entidades Diferentes")]
        [Trait("Categoria", "Core Domain Objects - Entity")]
        public void CompararEntidades_Diferentes_DeveRetornarTrue()
        {
            // Arrange feito no construtor

            // Act
            var saoDiferentes = _entidadeFakeValida != _entidadeFakeInvalida;

            // Assert
            Assert.True(saoDiferentes);
        }

        [Fact(DisplayName = "Entidade Nova Sem Erros")]
        [Trait("Categoria", "Core Domain Objects - Entity")]
        public void EntidadeNova_SemErros_DeveTerZeroErros()
        {
            // Arrange & Act feitos no construtor

            // Assert
            Assert.Equal(0, _entidadeFakeValida.QuantidadeErros);
        }

        [Fact(DisplayName = "Entidade Nova Com Erros")]
        [Trait("Categoria", "Core Domain Objects - Entity")]
        public void EntidadeNova_ComErros_DeveTerErros()
        {
            // Arrange feito no construtor

            // Act
            _entidadeFakeInvalida.EhValido();

            // Assert
            Assert.Equal(1, _entidadeFakeInvalida.QuantidadeErros);
        }
    }

    public class EntidadeFake : Entity
    {
        public int Quantidade { get; }

        public EntidadeFake(int quantidade)
        {
            Quantidade = quantidade;
        }

        public override bool EhValido()
        {
            ValidationResult = new EntidadeFakeValidator().Validate(this);

            var ehValido = ValidationResult.IsValid;

            return ehValido;
        }
    }

    public class EntidadeFakeValidator : AbstractValidator<EntidadeFake>
    {
        public EntidadeFakeValidator()
        {
            RuleFor(e => e.Quantidade).GreaterThan(0);
        }
    }

    public class FakeEvent : Event
    {
    }
}
