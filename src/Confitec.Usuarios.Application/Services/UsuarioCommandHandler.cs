using AutoMapper;
using Confitec.Usuarios.Application.Commands;
using Confitec.Usuarios.Core.Messages;
using Confitec.Usuarios.Domain.Usuario;
using FluentValidation.Results;
using MediatR;

namespace Confitec.Usuarios.Application.Services
{
    public class UsuarioCommandHandler : CommandHandler,
        IRequestHandler<CriarUsuarioCommand, ValidationResult>,
        IRequestHandler<AlterarUsuarioCommand, ValidationResult>,
        IRequestHandler<DeletarUsuarioCommand, ValidationResult>

    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<ValidationResult> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!request.Validar()) return request.ValidationResult;

            if (_usuarioRepository.ExisteUsuarioPorFiltro(request.Nome, request.SobreNome, request.Email).Result)
            {
                AdicionarErro("Já existe Usuário com esse Nome e E-mail!");
                return ValidationResult;
            }

            var usuario = Usuario.UsuarioFactory.NovoUsuario(request.Nome, request.SobreNome, request.Email, request.DataNascimento, request.Escolaridade);

            _usuarioRepository.Adicionar(usuario);

            return await PersistirDados(_usuarioRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AlterarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!request.Validar()) return request.ValidationResult;

            var usuario =  await _usuarioRepository.ObterUsuarioPorId(request.Id);

            if (usuario == null)
            {
                AdicionarErro("Usuário não encontrado!");
                return ValidationResult;
            }

            usuario.Alterar(request.Nome, request.SobreNome, request.Email, request.DataNascimento, request.Escolaridade);

            _usuarioRepository.Atualizar(usuario);

            return await PersistirDados(_usuarioRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(DeletarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!request.Validar()) return request.ValidationResult;

            var usuario = await _usuarioRepository.ObterUsuarioPorId(request.Id);

            if (usuario == null)
            {
                AdicionarErro("Usuário não encontrado!");
                return ValidationResult;
            }

            _usuarioRepository.Deletar(usuario);

            return await PersistirDados(_usuarioRepository.UnitOfWork);
        }
    }
}
