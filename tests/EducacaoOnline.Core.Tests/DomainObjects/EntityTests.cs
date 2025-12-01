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
            // Arrange
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
            // Act
            var saoIguais = _entidadeFakeValida == _entidadeFakeInvalida;

            // Assert
            Assert.False(saoIguais);
        }

        [Fact(DisplayName = "Comparar Entidades Diferentes")]
        [Trait("Categoria", "Core Domain Objects - Entity")]
        public void CompararEntidades_Diferentes_DeveRetornarTrue()
        {
            // Act
            var saoDiferentes = _entidadeFakeValida != _entidadeFakeInvalida;

            // Assert
            Assert.True(saoDiferentes);
        }

        [Fact(DisplayName = "Entidade Nova Sem Erros")]
        [Trait("Categoria", "Core Domain Objects - Entity")]
        public void EntidadeNova_SemErros_DeveTerZeroErros()
        {
            // Assert
            Assert.Equal(0, _entidadeFakeValida.QuantidadeErros);
        }

        [Fact(DisplayName = "Entidade Nova Com Erros")]
        [Trait("Categoria", "Core Domain Objects - Entity")]
        public void EntidadeNova_ComErros_DeveTerErros()
        {
            // Act
            _entidadeFakeInvalida.EhValido();

            // Assert
            Assert.Equal(1, _entidadeFakeInvalida.QuantidadeErros);
        }

        [Fact(DisplayName = "Entidades com mesmo Id são iguais")]
        [Trait("Categoria", "Core Domain Objects - Entity")]
        public void Entidades_ComMesmoId_DevemSerIguais()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var a = new EntidadeComId(id);
            var b = new EntidadeComId(id);

            // Assert
            Assert.True(a.Equals(b));
            Assert.True(a == b);
            Assert.False(a != b);
            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }

        [Fact(DisplayName = "Equals com null retorna false")]
        [Trait("Categoria", "Core Domain Objects - Entity")]
        public void Equals_ComNull_DeveRetornarFalse()
        {
            // Arrange & Act
            var a = new EntidadeComId(Guid.NewGuid());

            // Assert
            Assert.False(a.Equals(null));
        }

        [Fact(DisplayName = "Operador == com ambos null retorna true")]
        [Trait("Categoria", "Core Domain Objects - Entity")]
        public void OperadorIgual_AmbosNull_DeveRetornarTrue()
        {
            // Arrange & Act
            Entity? a = null;
            Entity? b = null;

            // Assert
            Assert.True(a == b);
            Assert.False(a != b);
        }

        [Fact(DisplayName = "Operador == com um null retorna false")]
        [Trait("Categoria", "Core Domain Objects - Entity")]
        public void OperadorIgual_UmNull_DeveRetornarFalse()
        {
            // Arrange & Act
            Entity? a = null;
            var b = new EntidadeComId(Guid.NewGuid());

            // Assert
            Assert.False(a == b);
            Assert.True(a != b);
        }

        [Fact(DisplayName = "GetHashCode consistente para mesmo Id")]
        [Trait("Categoria", "Core Domain Objects - Entity")]
        public void GetHashCode_IdsIguais_DevemSerIguais()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var a = new EntidadeComId(id);
            var b = new EntidadeComId(id);

            // Assert
            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }

        [Fact(DisplayName = "ToString retorna formato esperado")]
        [Trait("Categoria", "Core Domain Objects - Entity")]
        public void ToString_DeveRetornarFormatoEsperado()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entidade = new EntidadeComId(id);

            // Act
            var esperado = $"{entidade.GetType().Name} [Id={id}]";

            // Assert
            Assert.Equal(esperado, entidade.ToString());
        }

        [Fact(DisplayName = "EhValido padrão da base retorna true")]
        [Trait("Categoria", "Core Domain Objects - Entity")]
        public void EhValido_Padrao_RetornaTrue()
        {
            // Arrange & Act
            var simple = new EntidadeSemValidacao();

            // Assert
            Assert.True(simple.EhValido());
        }

        private class EntidadeComId : Entity
        {
            public EntidadeComId(Guid id) : base(id) { }
        }

        private class EntidadeSemValidacao : Entity { }

        private class EntidadeFake : Entity
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

        private class EntidadeFakeValidator : AbstractValidator<EntidadeFake>
        {
            public EntidadeFakeValidator()
            {
                RuleFor(e => e.Quantidade).GreaterThan(0);
            }
        }

        private class FakeEvent : Event { }
    }
}