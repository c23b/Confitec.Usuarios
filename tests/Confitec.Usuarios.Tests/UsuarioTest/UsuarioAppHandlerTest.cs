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
using Confitec.Usuarios.Application.Services;
using Moq.AutoMock;
using Moq;

namespace Confitec.Usuarios.Tests.UsuarioTest
{
    [Collection(nameof(UsuarioCollection))]
    public class UsuarioAppHandlerTest
    {
        private readonly UsuarioTestFixture _usuarioTestFixture;
        private readonly AutoMocker _mocker;
        private readonly UsuarioCommandHandler _usuarioCommandHandler;

        public UsuarioAppHandlerTest(UsuarioTestFixture usuarioTestFixture)
        {
            _usuarioTestFixture = usuarioTestFixture;
            _mocker = new AutoMocker();
            _usuarioCommandHandler = _mocker.CreateInstance<UsuarioCommandHandler>();
        }

        [Fact(DisplayName = "UsuarioHandler - Criar Usuario Válido")]
        public async void CriarUsuarioCommand_ComandoValido()
        {
            //Arrange
            var usuaioCommand = new CriarUsuarioCommand("Bruno", "Silva", "bruno.silva@confitec.com", DateTime.Now, EscolaridadeEnum.Infantil);
            _mocker.GetMock<IUsuarioRepository>().Setup(r => r.ExisteUsuarioPorFiltro(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>())).Returns(Task.FromResult(false));
            _mocker.GetMock<IUsuarioRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            //Act
            var result = await _usuarioCommandHandler.Handle(usuaioCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.ExisteUsuarioPorFiltro(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>()), Times.Once);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.Adicionar(It.IsAny<Usuario>()), Times.Once);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "UsuarioHandler - Criar Usuario Inválido")]
        public async void CriarUsuarioCommand_ComandoInvalido()
        {
            //Arrange
            var usuaioCommand = new CriarUsuarioCommand("Bruno", "Silva", "bruno.silva@confitec.com", DateTime.Now, EscolaridadeEnum.Infantil);
            _mocker.GetMock<IUsuarioRepository>().Setup(r => r.ExisteUsuarioPorFiltro(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>()))
                                                 .Returns(Task.FromResult(true));
            //Act
            var result = await _usuarioCommandHandler.Handle(usuaioCommand, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains("Já existe Usuário com esse Nome e E-mail!", result.Errors.Select(c => c.ErrorMessage));
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.ExisteUsuarioPorFiltro(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>()), Times.Once);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.Adicionar(It.IsAny<Usuario>()), Times.Never);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

        [Fact(DisplayName = "UsuarioHandler - Alterar Usuario Válido")]
        public async void AlterarUsuarioCommand_ComandoValido()
        {
            //Arrange
            var usuario = _usuarioTestFixture.GerarUsuario(EscolaridadeEnum.Infantil);
            var usuaioCommand = new AlterarUsuarioCommand(usuario.Id, usuario.Nome, usuario.SobreNome, usuario.Email,
                                                          usuario.DataNascimento, usuario.Escolaridade);

            _mocker.GetMock<IUsuarioRepository>().Setup(r => r.ObterUsuarioPorId(It.IsAny<Guid>())).Returns(Task.FromResult(usuario));
            _mocker.GetMock<IUsuarioRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            //Act
            var result = await _usuarioCommandHandler.Handle(usuaioCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.ObterUsuarioPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.Atualizar(It.IsAny<Usuario>()), Times.Once);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "UsuarioHandler - Alterar Usuario Inválido")]
        public async void AlterarUsuarioCommand_ComandoInvalido()
        {
            //Arrange
            var usuario = _usuarioTestFixture.GerarUsuario(EscolaridadeEnum.Infantil);
            var usuaioCommand = new AlterarUsuarioCommand(usuario.Id, usuario.Nome, usuario.SobreNome, usuario.Email,
                                                          usuario.DataNascimento, usuario.Escolaridade);

            _mocker.GetMock<IUsuarioRepository>().Setup(r => r.ObterUsuarioPorId(It.IsAny<Guid>())).Returns(Task.FromResult((Usuario)null));
            //Act
            var result = await _usuarioCommandHandler.Handle(usuaioCommand, CancellationToken.None);


            //Assert
            Assert.False(result.IsValid);
            Assert.Contains("Usuário não encontrado!", result.Errors.Select(c => c.ErrorMessage));
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.ObterUsuarioPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.Atualizar(It.IsAny<Usuario>()), Times.Never);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

        [Fact(DisplayName = "UsuarioHandler - Deletar Usuario Válido")]
        public async void DeletarUsuarioCommand_ComandoValido()
        {
            //Arrange
            var usuario = _usuarioTestFixture.GerarUsuario(EscolaridadeEnum.Infantil);
            var usuaioCommand = new DeletarUsuarioCommand(usuario.Id);
            _mocker.GetMock<IUsuarioRepository>().Setup(r => r.ObterUsuarioPorId(It.IsAny<Guid>())).Returns(Task.FromResult(usuario));
            _mocker.GetMock<IUsuarioRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            //Act
            var result = await _usuarioCommandHandler.Handle(usuaioCommand, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.ObterUsuarioPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.Deletar(It.IsAny<Usuario>()), Times.Once);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "UsuarioHandler - Deletar Usuario Inválido")]
        public async void DeletarUsuarioCommand_ComandoInvalido()
        {
            //Arrange
            var usuario = _usuarioTestFixture.GerarUsuario(EscolaridadeEnum.Infantil);
            var usuaioCommand = new DeletarUsuarioCommand(usuario.Id);
            _mocker.GetMock<IUsuarioRepository>().Setup(r => r.ObterUsuarioPorId(It.IsAny<Guid>())).Returns(Task.FromResult((Usuario)null));

            //Act
            var result = await _usuarioCommandHandler.Handle(usuaioCommand, CancellationToken.None);


            //Assert
            Assert.False(result.IsValid);
            Assert.Contains("Usuário não encontrado!", result.Errors.Select(c => c.ErrorMessage));
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.ObterUsuarioPorId(It.IsAny<Guid>()), Times.Once);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.Deletar(It.IsAny<Usuario>()), Times.Never);
            _mocker.GetMock<IUsuarioRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }
    }
}
