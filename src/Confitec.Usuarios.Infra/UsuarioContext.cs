using Confitec.Usuarios.Core.Data;
using Confitec.Usuarios.Core.Messages;
using Confitec.Usuarios.Domain.Usuario;
using Confitec.Usuarios.DomainObjects;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Confitec.Usuarios.Infra
{
    public class UsuarioContext : DbContext, IUnitOfWork
    {       

        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options)
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public async Task<bool> Commit()
        {
            var sucesso = await base.SaveChangesAsync() > 0;          

            return sucesso;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsuarioContext).Assembly);            

            base.OnModelCreating(modelBuilder);
        }
    }
        
}