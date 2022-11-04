using Confitec.Usuarios.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confitec.Usuarios.Application.Validations
{
    public class DeletarUsuarioValidation : AbstractValidator<DeletarUsuarioCommand>
    {
        public static string UsuarioNaoInformado => "Usuário não Informado!";

        public DeletarUsuarioValidation()
        {
            RuleFor(x => x.Id)
            .NotNull().WithMessage(UsuarioNaoInformado)
            .NotEmpty().WithMessage(UsuarioNaoInformado);
        }
    }
}
