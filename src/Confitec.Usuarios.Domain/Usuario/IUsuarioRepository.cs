using Confitec.Usuarios.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confitec.Usuarios.Domain.Usuario
{
    public interface IUsuarioRepository : IRepository<Usuario>
    { 
        void Adicionar(Usuario usuario);

        void Atualizar(Usuario usuario);

        void Deletar(Usuario usuario);

        Task<IEnumerable<Usuario>> ObterUsuarios();

        Task<Usuario?> ObterUsuarioPorId(Guid id);

        Task<bool> ExisteUsuarioPorFiltro(string nome, string sobreNome, string email);
    }
}
