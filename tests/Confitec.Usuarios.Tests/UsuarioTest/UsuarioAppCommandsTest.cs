using Confitec.Usuarios.Application.Commands;
using Confitec.Usuarios.Core.DomainObjects;
using Confitec.Usuarios.Domain.Usuario;
using Confitec.Usuarios.UsuarioTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confitec.Usuarios.Application.Validations;

namespace Confitec.Usuarios.Tests.UsuarioTest
{
    [Collection(nameof(UsuarioCollection))]
    public class UsuarioAppCommandsTest
    {
        private readonly UsuarioTestFixture _usuarioTestFixture;

        public UsuarioAppCommandsTest(UsuarioTestFixture usuarioTestFixture)
        {
            _usuarioTestFixture = usuarioTestFixture;
        }

        [Fact(DisplayName = "UsuarioCommands - Criar Usuario Válido")]
        public void CriarUsuarioCommand_ComandoValido()
        {
            //Arrange
            var usuaioCommand = new CriarUsuarioCommand("Bruno", "Silva", "bruno.silva@confitec.com", DateTime.Now, EscolaridadeEnum.Infantil);

            //Act
            var result = usuaioCommand.Validar();

            //Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "UsuarioCommands - Criar Usuario Inválido")]
        public void CriarUsuarioCommand_ComandoInvalido()
        {
            //Arrange
            var usuaioCommand = new CriarUsuarioCommand("", "", "", DateTime.Now.AddDays(100), (EscolaridadeEnum)99);

            //Act
            var result = usuaioCommand.Validar();

            //Assert
            Assert.False(result);
            Assert.Contains(CriarUsuarioValidation.NomeInvalido, usuaioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CriarUsuarioValidation.SobreNomeInvalido, usuaioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CriarUsuarioValidation.EmailInvalido, usuaioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CriarUsuarioValidation.DataNascimentoInvalido, usuaioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(CriarUsuarioValidation.EscolaridadeInvalido, usuaioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }

        [Fact(DisplayName = "UsuarioCommands - Alterar Usuario Válido")]
        public void AlterarUsuarioCommand_ComandoValido()
        {
            //Arrange
            var usuario = _usuarioTestFixture.GerarUsuario(EscolaridadeEnum.Infantil);
            var usuaioCommand = new AlterarUsuarioCommand(usuario.Id, usuario.Nome, usuario.SobreNome, usuario.Email, 
                                                          usuario.DataNascimento, usuario.Escolaridade);
            //Act
            var result = usuaioCommand.Validar();

            //Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "UsuarioCommands - Alterar Usuario Inválido")]
        public void AlterarUsuarioCommand_ComandoInvalido()
        {
            //Arrange
            var usuaioCommand = new AlterarUsuarioCommand(Guid.Empty, "", "", "", DateTime.Now.AddDays(100), (EscolaridadeEnum)99);

            //Act
            var result = usuaioCommand.Validar();

            //Assert
            Assert.False(result);
            Assert.Contains(AlterarUsuarioValidation.UsuarioNaoInformado, usuaioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AlterarUsuarioValidation.NomeInvalido, usuaioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AlterarUsuarioValidation.SobreNomeInvalido, usuaioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AlterarUsuarioValidation.EmailInvalido, usuaioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AlterarUsuarioValidation.DataNascimentoInvalido, usuaioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AlterarUsuarioValidation.EscolaridadeInvalido, usuaioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }

        [Fact(DisplayName = "UsuarioCommands - Deletar Usuario Válido")]
        public void DeletarUsuarioCommand_ComandoValido()
        {
            //Arrange
            var usuaioCommand = new DeletarUsuarioCommand(Guid.NewGuid());

            //Act
            var result = usuaioCommand.Validar();

            //Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "UsuarioCommands - Deletar Usuario Inválido")]
        public void DeletarUsuarioCommand_ComandoInvalido()
        {
            //Arrange
            var usuaioCommand = new DeletarUsuarioCommand(Guid.Empty);

            //Act
            var result = usuaioCommand.Validar();

            //Assert
            Assert.False(result);
            Assert.Contains(DeletarUsuarioValidation.UsuarioNaoInformado, usuaioCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
