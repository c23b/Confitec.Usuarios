
using Confitec.Usuarios.Core.Data;
using Confitec.Usuarios.Domain.Usuario;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confitec.Usuarios.Infra.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuarioContext _context;

        public UsuarioRepository(UsuarioContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(Usuario usuario)
        {
            _context.AddAsync(usuario);
        }

        public void Atualizar(Usuario usuario)
        {
            _context.Update(usuario);
        }

        public void Deletar(Usuario usuario)
        {
            _context.Remove(usuario);
        }
        public async Task<IEnumerable<Usuario>> ObterUsuarios()
        {
           return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> ObterUsuarioPorId(Guid id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<bool> ExisteUsuarioPorFiltro(string nome, string sobreNome, string email)
        {
            return await _context.Usuarios.AnyAsync(x => x.Nome == nome
                                                      && x.SobreNome == sobreNome
                                                      && x.Email == email);

        }

        public void Dispose()
        {
            _context?.Dispose();
        }

    }
}
