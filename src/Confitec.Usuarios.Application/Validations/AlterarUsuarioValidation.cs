using Confitec.Usuarios.Application.Commands;
using Confitec.Usuarios.Core.Util;
using Confitec.Usuarios.Domain.Usuario;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confitec.Usuarios.Application.Validations
{
    public class AlterarUsuarioValidation : AbstractValidator<AlterarUsuarioCommand>
    {
        public static string UsuarioNaoInformado => "Usuário não Informado!";
        public static string NomeInvalido => "Nome Inválido!";
        public static string SobreNomeInvalido => "Sobre Nome Inválido!";
        public static string EmailInvalido => "E-mail Inválido!";
        public static string DataNascimentoInvalido => "Data de Nascimento Inválida!";
        public static string EscolaridadeInvalido => "Escolaridade Inválida!";

        public AlterarUsuarioValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(UsuarioNaoInformado)
                .NotNull().WithMessage(UsuarioNaoInformado);

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage(NomeInvalido)
                .NotNull().WithMessage(NomeInvalido);

            RuleFor(x => x.SobreNome)
                .NotEmpty().WithMessage(SobreNomeInvalido)
                .NotNull().WithMessage(SobreNomeInvalido);

            RuleFor(x => x.Email)
                .Must(ValidarEmail).WithMessage(EmailInvalido);

            RuleFor(x => x.DataNascimento)
                .LessThanOrEqualTo(DateTime.Now).WithMessage(DataNascimentoInvalido);

            RuleFor(x => x.Escolaridade)
                .Must(ValidarEscolaridade).WithMessage(EscolaridadeInvalido);
        }

        protected static bool ValidarEmail(string email)
        {
            return EmailRules.Validar(email);
        }

        protected static bool ValidarEscolaridade(EscolaridadeEnum escolaridade)
        {
            return Enum.IsDefined(typeof(EscolaridadeEnum), escolaridade);
        }
    }
}
