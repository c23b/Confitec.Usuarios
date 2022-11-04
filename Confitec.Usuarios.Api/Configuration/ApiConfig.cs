using Confitec.Usuarios.Application.AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Confitec.Usuarios.Api.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfig(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            //services.AddAutoMapper(typeof(UsuarioMappingProfile));
            services.AddMvcCore();
            services.AddCors();

            return services;
        }

        public static IApplicationBuilder UseApiConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            {
                options.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowAnyOrigin();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
    }
}
