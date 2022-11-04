using Confitec.Usuarios.Api.Configuration;
using Confitec.Usuarios.Infra;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.ResolveDependencies();
builder.Services.AddDbContext<UsuarioContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddApiConfig();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();
//var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//app.UseApiConfig(app.Environment);
//app.UseSwaggerConfig(apiVersionDescriptionProvider);

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiConfig();

builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerConfig();

builder.Services.ResolveDependencies();

var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseAuthentication();
app.UseApiConfig(app.Environment);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseSwaggerConfig(apiVersionDescriptionProvider);

app.Run();