using Confitec.Usuarios.Domain.Usuario;

namespace Confitec.Usuarios.Api.Models
{
    public class UsuarioModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public string SobreNome { get; set; }

        public string Email { get; set; }

        public DateTime DataNascimento { get; set; }

        public EscolaridadeEnum Escolaridade { get; set; }
    }
}
