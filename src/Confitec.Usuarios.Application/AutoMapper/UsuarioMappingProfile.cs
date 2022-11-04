using AutoMapper;
using Confitec.Usuarios.Application.Commands;
using Confitec.Usuarios.Application.DTOs;
using Confitec.Usuarios.Domain.Usuario;


namespace Confitec.Usuarios.Application.AutoMapper
{
    public class UsuarioMappingProfile : Profile
    {
        public UsuarioMappingProfile()
        {
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<IEnumerable<Usuario>, IEnumerable<UsuarioDTO>>().ReverseMap();
        }
    }
}
