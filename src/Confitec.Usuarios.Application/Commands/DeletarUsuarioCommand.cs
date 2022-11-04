using Confitec.Usuarios.Application.Validations;
using Confitec.Usuarios.Core.Messages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confitec.Usuarios.Application.Commands
{
    public class DeletarUsuarioCommand : Command
    {
        public Guid Id { get; set; }
     
        public DeletarUsuarioCommand(Guid id)
        {
            Id = id;
        }

        public override bool Validar()
        {
            ValidationResult = new DeletarUsuarioValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        
    }
}
