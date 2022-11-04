using Confitec.Usuarios.Domain.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confitec.Usuarios.UsuarioTest
{
    [CollectionDefinition(nameof(UsuarioCollection))]
    public class UsuarioCollection : ICollectionFixture<UsuarioTestFixture> { }

    public class UsuarioTestFixture : IDisposable
    {

        public UsuarioTestFixture()
        {
            
        }

        public Usuario GerarUsuario(EscolaridadeEnum escolaridade)
        {
            return Usuario.UsuarioFactory.NovoUsuario("Bruno", "Silva", "bruno.silva@confitec.com", DateTime.Now, escolaridade);
        }

        public Usuario GerarUsuario(string nome, string sobreNome, string email, DateTime dataNascimento, EscolaridadeEnum escolaridade)
        {
            return Usuario.UsuarioFactory.NovoUsuario(nome, sobreNome, email, dataNascimento, escolaridade);
        }

        

        public void Dispose()
        {

        }
    }
}
