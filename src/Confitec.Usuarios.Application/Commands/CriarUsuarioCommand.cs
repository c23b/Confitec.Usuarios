using Confitec.Usuarios.Application.Validations;
using Confitec.Usuarios.Core.Messages;
using Confitec.Usuarios.Core.Util;
using Confitec.Usuarios.Domain.Usuario;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confitec.Usuarios.Application.Commands
{
    public class CriarUsuarioCommand : Command
    {
        public string Nome { get; set; }

        public string SobreNome { get; set; }

        public string Email { get; set; }

        
        public DateTime DataNascimento { get; set; }

        public EscolaridadeEnum Escolaridade { get; set; }

        public CriarUsuarioCommand(string nome, string sobreNome, string email, DateTime dataNascimento, EscolaridadeEnum escolaridade)
        {
            Nome = nome;
            SobreNome = sobreNome;
            Email = email;
            DataNascimento = dataNascimento;
            Escolaridade = escolaridade;
        }

        public override bool Validar()
        {
            ValidationResult = new CriarUsuarioValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        
    }
}
