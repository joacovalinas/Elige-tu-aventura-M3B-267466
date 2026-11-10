
using EscogeAventura.LogicaAccesoDatos;
using EscogeAventura.LogicaAccesoDatos.Repos;
using EscogeAventura.LogicaAplicacion.CasosUso.CUCategoria;
using EscogeAventura.LogicaAplicacion.CasosUso.CUHistoria;
using EscogeAventura.LogicaAplicacion.CasosUso.CULectura;
using EscogeAventura.LogicaAplicacion.CasosUso.CUUsuario;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUCategoria;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUHistoria;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICULectura;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUUsuario;
using EscogeAventura.LogicaNegocios.IRepositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace EscogeAventura.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            // Configurar la cadena de conexión (desde appsettings.json)
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");//DefaultConnection debe coincidir con el nombre designado en el JSON.
            // Registrar el DbContext en el contenedor de servicios
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));





            // REPOSITORIOS
            builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
            builder.Services.AddScoped<IRepositorioRol, RepositorioRol>();
            builder.Services.AddScoped<IRepositorioHistoria, RepositorioHistoria>();
            builder.Services.AddScoped<IRepositorioCategoria, RepositorioCategoria>();
            builder.Services.AddScoped<IRepositorioCapitulo, RepositorioCapitulo>();
            builder.Services.AddScoped<IRepositorioLectura, RepositorioLectura>();
            builder.Services.AddScoped<IRepositorioAuditoria, RepositorioAuditoria>();


            // CASOS DE USO


            builder.Services.AddScoped<ICUVerifExistencia, CUVerifExistencia>();
            builder.Services.AddScoped<ICUListarHistoriasPublicadas, CUListarHistoriasPublicadas>();
            builder.Services.AddScoped<ICUObtenerCategorias, CUObtenerCategorias>();
            builder.Services.AddScoped<ICUAltaLectura, CUAltaLectura>();
            builder.Services.AddScoped<ICUObtenerLectura, CUObtenerLectura>();
            builder.Services.AddScoped<ICUAvanzarLectura, CUAvanzarLectura>();
            builder.Services.AddScoped<ICULeerLectura, CULeerLectura>();
            builder.Services.AddScoped<ICUObtenerHistorial, CUObtenerHistorial>();
            builder.Services.AddScoped<ICURankingHistorias, CURankingHistorias>();
            builder.Services.AddScoped<ICUFinalAlcanzado, CUFinalAlcanzado>();
            builder.Services.AddScoped<ICURankingUsuarios, CURankingUsuarios>();

            //JWT


            var clave = "UTzl^7yPl$5xrT6&{o89$CW1XXRrN(>XqIp{W4s2S5$>KT$6CG!2M]'ZlrqH-t%eI4.X9W~u#qO+oX£+[?7QDAa";
            var claveCodificada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clave));


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddJwtBearer(opt =>
          {
              opt.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateIssuer = false,
                  ValidateAudience = false,
                  ValidateLifetime = true,
                  ValidateIssuerSigningKey = true,
                  IssuerSigningKey = claveCodificada,
                  RoleClaimType = ClaimTypes.Role,
                  NameClaimType = ClaimTypes.Name
              };

              opt.Events = new JwtBearerEvents
              {
                  OnAuthenticationFailed = contexto =>
                  {
                      Console.WriteLine("JWT FALLÓ: " + contexto.Exception.Message);
                      return Task.CompletedTask;
                  }
              };
          });


            builder.Services.AddAuthorization();


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("politicaEspecifica", builder =>
                {
                    builder.AllowAnyOrigin() // cambiar por nombre web app
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

          

            app.UseCors("politicaEspecifica");
            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();
          

            app.MapControllers();

            app.Run();
        }
    }
}
