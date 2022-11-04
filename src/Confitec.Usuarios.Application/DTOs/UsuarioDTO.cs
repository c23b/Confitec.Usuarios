using Confitec.Usuarios.Domain.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confitec.Usuarios.Application.DTOs
{
    public class UsuarioDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public string SobreNome { get; set; }

        public string Email { get; set; }

        public DateTime DataNascimento { get; set; }

        public EscolaridadeEnum Escolaridade { get; set; }
    }
}
