using Confitec.Usuarios.DomainObjects;
using Confitec.Usuarios.Core.Util;
using System.Collections.Specialized;
using Confitec.Usuarios.Core.DomainObjects;

namespace Confitec.Usuarios.Domain.Usuario
{
    public class Usuario : Entity
    {    
        public string Nome { get; private set; }

        public string SobreNome { get; private set; }

        public string Email { get; private set; }

        public DateTime DataNascimento { get; private set; }

        public EscolaridadeEnum Escolaridade { get; private set; }

        protected Usuario(string nome, string sobreNome, string email, DateTime dataNascimento, EscolaridadeEnum escolaridade)
        {
            ValidarUsuario(nome, sobreNome, email, dataNascimento, escolaridade);
            Nome = nome;
            SobreNome = sobreNome;
            Email = email;
            DataNascimento = dataNascimento;
            Escolaridade = escolaridade;
        }

        public void Alterar(string nome, string sobreNome, string email, DateTime dataNascimento, EscolaridadeEnum escolaridade)
        {
            ValidarUsuario(nome, sobreNome, email, dataNascimento, escolaridade);

            Nome = nome;
            SobreNome = sobreNome;
            Email = email;
            DataNascimento = dataNascimento;
            Escolaridade = escolaridade;
                            
        }        

        private void ValidarUsuario(string nome, string sobreNome, string email, DateTime dataNascimento, EscolaridadeEnum escolaridade)
        {
            if (string.IsNullOrEmpty(nome))
                throw new DomainException("Nome Inválido");

            if (string.IsNullOrEmpty(sobreNome))
                throw new DomainException("Sobre Nome Inválido");

            if(!EmailRules.Validar(email))
                throw new DomainException("E-mail Inválido");

            if (dataNascimento > DateTime.Now)
                throw new DomainException("Data Nascimento Inválida");

            if (!Enum.IsDefined(typeof(EscolaridadeEnum), escolaridade))
                throw new DomainException("Escolaridade Inválida");
        }

        public static class UsuarioFactory
        {
            public static Usuario NovoUsuario(string nome, string sobreNome, string email, DateTime dataNascimento, EscolaridadeEnum escolaridade)
            {
                return new Usuario(nome, sobreNome, email, dataNascimento, escolaridade);
            }
        }
    }
}