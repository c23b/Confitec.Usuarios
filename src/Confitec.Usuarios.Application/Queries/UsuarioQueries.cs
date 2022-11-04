using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Confitec.Usuarios.Application.DTOs;
using Confitec.Usuarios.Domain.Usuario;


namespace Confitec.Usuarios.Application.Queries
{
    public interface IUsuarioQueries
    {
        Task<IEnumerable<UsuarioDTO>> ObterUsuarios();
    }

    public class UsuarioQueries : IUsuarioQueries
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioQueries(IMapper mapper, IUsuarioRepository usuarioRepository)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<UsuarioDTO>> ObterUsuarios()
        {
            var result = await _usuarioRepository.ObterUsuarios();
            return result.Select(x => _mapper.Map<UsuarioDTO>(x));
        }

        
    }

}