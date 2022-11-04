using AutoMapper;
using Confitec.Usuarios.Application.Queries;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Confitec.Usuarios.Infra.Repository;
using MediatR;
using Confitec.Usuarios.Infra;
using Confitec.Usuarios.Application.Commands;
using Confitec.Usuarios.Application.Services;
using Confitec.Usuarios.Domain.Usuario;
using FluentValidation.Results;
using Confitec.Usuarios.Application.AutoMapper;

namespace Confitec.Usuarios.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        //public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        //{
        //    services.AddScoped<UsuarioContext>();
        //    services.AddScoped<IUnitOfWork, UsuarioContext>();

        //    #region AutoMapper

        //    var mappingConfig = new MapperConfiguration(mc =>
        //    {
        //        mc.AddProfile(new UsuarioMappingProfile());
        //    });
        //    IMapper mapper = mappingConfig.CreateMapper();

        //    #endregion

        //    services.AddScoped<IUsersAppService, UsersAppService>();
        //    services.AddScoped<IUsersRepository, UsersRepository>();


        //    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        //    //services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        //    services.AddTransient<ConfigureSwaggerOptions>();

        //    return services;
        //}
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {

            //applicatiob
            services.AddScoped<IRequestHandler<CriarUsuarioCommand, ValidationResult>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<AlterarUsuarioCommand, ValidationResult>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<DeletarUsuarioCommand, ValidationResult>, UsuarioCommandHandler>();

            var assemply = AppDomain.CurrentDomain.Load("Confitec.Usuarios.Application");
            //AutoMapper
            services.AddAutoMapper(assemply);
            
            //MediatR
            services.AddMediatR(assemply);

            //Infra
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<UsuarioContext>();
            services.AddScoped<IUsuarioQueries, UsuarioQueries>();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddTransient<ConfigureSwaggerOptions>();


            return services;
        }

    }
}