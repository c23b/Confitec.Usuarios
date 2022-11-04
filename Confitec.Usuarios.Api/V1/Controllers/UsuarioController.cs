using Confitec.Usuarios.Api.Models;
using Confitec.Usuarios.Application.Commands;
using Confitec.Usuarios.Application.DTOs;
using Confitec.Usuarios.Application.Queries;
using Confitec.Usuarios.Core.Controllers;
using Confitec.Usuarios.Domain.Usuario;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Iatec.Paaeb.Api.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/usuarios")]
    public class UsuarioController : MainController
    {

        private readonly ILogger<UsuarioController> _logger;
        private readonly IMediator _mediatorHandler;
        private readonly IUsuarioQueries _usuarioQueries;


        public UsuarioController(ILogger<UsuarioController> logger, IMediator mediatorHandler, IUsuarioQueries usuarioQueries)
        {
            _logger = logger;
            _mediatorHandler = mediatorHandler;
            _usuarioQueries = usuarioQueries;

        }

        [HttpGet(Name = "ObterUsuarios")]
        public async Task<IEnumerable<UsuarioDTO>> Get()
        {
            return await _usuarioQueries.ObterUsuarios();
        }

        [HttpPost(Name = "CriarUsuario")]
        public async Task<IActionResult> Post(UsuarioModel usuario)
        {
            var command = new CriarUsuarioCommand(usuario.Nome, usuario.SobreNome, usuario.Email, usuario.DataNascimento, usuario.Escolaridade);
            var result = await _mediatorHandler.Send(command, CancellationToken.None);

            return CustomResponse(result);
        }

        [HttpPut(Name = "AlterarUsuario")]
        public async Task<IActionResult> Put(UsuarioModel usuario)
        {
            var command = new AlterarUsuarioCommand(usuario.Id, usuario.Nome, usuario.SobreNome, usuario.Email, usuario.DataNascimento, usuario.Escolaridade);

            var result = await _mediatorHandler.Send(command, CancellationToken.None);

            return CustomResponse(result);
        }

        [HttpDelete(Name = "DeleteUsuario")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediatorHandler.Send(new DeletarUsuarioCommand(id), CancellationToken.None);

            return CustomResponse(result);
        }                
    }
}
