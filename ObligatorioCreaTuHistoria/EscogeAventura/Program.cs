using EscogeAventura.LogicaAccesoDatos;
using EscogeAventura.LogicaAccesoDatos.Repos;
using EscogeAventura.LogicaAplicacion.CasosUso;
using EscogeAventura.LogicaAplicacion.CasosUso.CUCapitulo;
using EscogeAventura.LogicaAplicacion.CasosUso.CUCategoria;
using EscogeAventura.LogicaAplicacion.CasosUso.CUHistoria;
using EscogeAventura.LogicaAplicacion.CasosUso.CURol;
using EscogeAventura.LogicaAplicacion.CasosUso.CUUsuario;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUCapitulo;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUCategoria;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUHistoria;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICURol;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUUsuario;
using EscogeAventura.LogicaNegocios.Entidades;
using EscogeAventura.LogicaNegocios.IRepositorios;
using EscogeAventura.LogicaNegocios.ValueObjects;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace EscogeAventura.WebApp
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

            builder.Services.AddSession();

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            //Inyeccion de repositorios
            builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
            builder.Services.AddScoped<IRepositorioRol, RepositorioRol>();
            builder.Services.AddScoped<IRepositorioHistoria, RepositorioHistoria>();
            builder.Services.AddScoped<IRepositorioCategoria, RepositorioCategoria>();
            builder.Services.AddScoped<IRepositorioCapitulo, RepositorioCapitulo>();
            builder.Services.AddScoped<IRepositorioHistoria, RepositorioHistoria>();

            //ID - CU
            builder.Services.AddScoped<ICUAltaUsuario, CUAltaUsuario>();
            builder.Services.AddScoped<ICUVerifExistencia, CUVerifExistencia>();
            builder.Services.AddScoped<ICUObtenerRol, CUObtenerRol>();
            builder.Services.AddScoped<ICUAltaHistoria, CUAltaHistoria>();
            builder.Services.AddScoped<ICUObtenerCategorias, CUObtenerCategorias>();
            builder.Services.AddScoped<ICUAltaCapitulo, CUAltaCapitulo>();
            builder.Services.AddScoped<ICUListarHistoria, CUListarHistoria>();




            var app = builder.Build();



            //CREAR USUARIO ADMINISTRADOR SI NO EXISTE

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();

                if (!db.Usuarios.Any(u => u.Email == "admin@admin.com"))
                {
                    var rol = db.Roles.First(r => r.Id == 1);
                    var rolAdmin = db.Roles.FirstOrDefault(r => r.Nombre == "admin");
                    var admin = new Usuario( "nombre",  "apellido",  "admin@admin.com",  "admin", "1234",  rolAdmin);

                    db.Usuarios.Add(admin);
                    db.SaveChanges();
                }
            }



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();






        }
    }
}
