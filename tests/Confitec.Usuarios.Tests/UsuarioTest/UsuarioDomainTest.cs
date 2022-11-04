using Confitec.Usuarios.Core.DomainObjects;
using Confitec.Usuarios.Domain.Usuario;
using Confitec.Usuarios.UsuarioTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confitec.Usuarios.Tests.UsuarioTest
{
    [Collection(nameof(UsuarioCollection))]
    public class UsuarioDomainTest
    {
        private readonly UsuarioTestFixture _usuarioTestFixture;

        public UsuarioDomainTest(UsuarioTestFixture usuarioTestFixture)
        {
            _usuarioTestFixture = usuarioTestFixture;
        }

        [Fact(DisplayName ="Usuario - Novo Usuario Valido")]
        public void NovoUsuario_Valido()
        {
            //Arrange
            //Act
            var usuario = _usuarioTestFixture.GerarUsuario(EscolaridadeEnum.Infantil);

            //Assert
            Assert.NotNull(usuario);
        }

        [Fact(DisplayName = "Usuario - Novo Usuario Inválido")]
        public void NovoUsuario_Invalido()
        {
            //Arrange
            //Act       
            //Assert
            Assert.Throws<DomainException>(() => Usuario.UsuarioFactory.NovoUsuario("", "Silva", "bruno.silva@confitec.com", DateTime.Now, EscolaridadeEnum.Médio));
            Assert.Throws<DomainException>(() => Usuario.UsuarioFactory.NovoUsuario("Bruno", null, "bruno.silva@confitec.com", DateTime.Now, EscolaridadeEnum.Médio));
            Assert.Throws<DomainException>(() => Usuario.UsuarioFactory.NovoUsuario("Bruno", "Silva", "", DateTime.Now, EscolaridadeEnum.Médio));
            Assert.Throws<DomainException>(() => Usuario.UsuarioFactory.NovoUsuario("Bruno", "Silva", "bruno.silva@confitec.com", DateTime.Now.AddDays(10), EscolaridadeEnum.Médio));
            Assert.Throws<DomainException>(() => Usuario.UsuarioFactory.NovoUsuario("Bruno", "Silva", "bruno.silva@confitec.com", DateTime.Now, (EscolaridadeEnum)10));
        }

        [Fact(DisplayName = "Usuario - Alterar Usuario Valido")]
        public void AlterarUsuario_Valido()
        {
            //Arrange
            var usuario = _usuarioTestFixture.GerarUsuario(EscolaridadeEnum.Infantil);
            var usuarioCopia = _usuarioTestFixture.GerarUsuario(EscolaridadeEnum.Infantil);

            //Act
            var newDate = DateTime.Now;
            usuario.Alterar("TESTE", "TESTE", "TESTE@confitec.com", new DateTime(2000, 01, 01), EscolaridadeEnum.Médio);

            //Assert
            Assert.True(usuario.Nome != usuarioCopia.Nome);
            Assert.True(usuario.SobreNome != usuarioCopia.SobreNome);
            Assert.True(usuario.Email != usuarioCopia.Email);
            Assert.True(usuario.DataNascimento != usuarioCopia.DataNascimento);
            Assert.True(usuario.Escolaridade != usuarioCopia.Escolaridade);
        }

        [Fact(DisplayName = "Usuario - Falha Alterar Usuario")]
        public void AlterarUsuario_Falha()
        {
            //Arrange
            var usuario = _usuarioTestFixture.GerarUsuario(EscolaridadeEnum.Infantil);
            //Act       
            //Assert 
            Assert.Throws<DomainException>(() => usuario.Alterar("", "Silva", "bruno.silva@confitec.com", DateTime.Now, EscolaridadeEnum.Médio));
            Assert.Throws<DomainException>(() => usuario.Alterar("Bruno", null, "bruno.silva@confitec.com", DateTime.Now, EscolaridadeEnum.Médio));
            Assert.Throws<DomainException>(() => usuario.Alterar("Bruno", "Silva", "", DateTime.Now, EscolaridadeEnum.Médio));
            Assert.Throws<DomainException>(() => usuario.Alterar("Bruno", "Silva", "bruno.silva@confitec.com", DateTime.Now.AddDays(10), EscolaridadeEnum.Médio));
            Assert.Throws<DomainException>(() => usuario.Alterar("Bruno", "Silva", "bruno.silva@confitec.com", DateTime.Now, (EscolaridadeEnum)10));
        }

    }
}
