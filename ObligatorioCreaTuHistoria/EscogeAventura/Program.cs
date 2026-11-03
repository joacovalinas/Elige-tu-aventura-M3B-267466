using EscogeAventura.LogicaAccesoDatos;
using EscogeAventura.LogicaAccesoDatos.Repos;
using EscogeAventura.LogicaAplicacion.CasosUso;
using EscogeAventura.LogicaAplicacion.CasosUso.CUAuditoria;
using EscogeAventura.LogicaAplicacion.CasosUso.CUCapitulo;
using EscogeAventura.LogicaAplicacion.CasosUso.CUCategoria;
using EscogeAventura.LogicaAplicacion.CasosUso.CUHistoria;
using EscogeAventura.LogicaAplicacion.CasosUso.CURol;
using EscogeAventura.LogicaAplicacion.CasosUso.CUUsuario;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUAuditoria;
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
            builder.Services.AddScoped<IRepositorioLectura, RepositorioLectura>();
            builder.Services.AddScoped<IRepositorioAuditoria, RepositorioAuditoria>();


            //ID - CU
            builder.Services.AddScoped<ICUAltaUsuario, CUAltaUsuario>();
            builder.Services.AddScoped<ICUVerifExistencia,CUVerifExistencia>();
            builder.Services.AddScoped<ICUObtenerRol, CUObtenerRol>();
            builder.Services.AddScoped<ICUAltaHistoria, CUAltaHistoria>();
            builder.Services.AddScoped<ICUObtenerCategorias, CUObtenerCategorias>();
            builder.Services.AddScoped<ICUAltaCapitulo, CUAltaCapitulo>();
            builder.Services.AddScoped<ICUListarHistoria, CUListarHistoria>();
            builder.Services.AddScoped<ICUEditarHistoria, CUEditarHistoria>();
            builder.Services.AddScoped<ICUObtenerHistoria, CUObtenerHistoria>();
            builder.Services.AddScoped<ICUPublicar, CUPublicar>();  
            builder.Services.AddScoped<ICUBajaHistoria, CUBajaHistoria>();
            builder.Services.AddScoped<ICUAltaAuditoria, CUAltaAuditoria>();
            builder.Services.AddScoped<ICUListarAuditoria, CUListarAuditoria>();


            var app = builder.Build();



            //SEED

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();

                // Seed de roles
                if (!db.Roles.Any())
                {
                    db.Roles.AddRange(
                        new Rol { Nombre = "Admin" },
                        new Rol { Nombre = "Usuario" }
                    );
                    db.SaveChanges();
                }

                // Seed del usuario admin
                if (!db.Usuarios.Any(u => u.Email == "admin@admin.com"))
                {
                    var rolAdmin = db.Roles.First(r => r.Nombre == "Admin");
                    var admin = new Usuario("nombre", "apellido", "admin@admin.com", "admin", "1234", rolAdmin);

                    db.Usuarios.Add(admin);
                    db.SaveChanges();
                }
                var nombresCategorias = new[]
   {
        "Aventura",
        "Fantasía",
        "Ciencia ficción",
        "Terror",
        "Misterio",
        "Romance",
        "Histórica",
        "Humor",
        "Drama",
        "Acción"
    };

                var existentes = db.Categorias.Select(c => c.Nombre).ToHashSet();

                var nuevas = nombresCategorias
                    .Where(n => !existentes.Contains(n))
                    .Select(n => new Categoria { Nombre = n })
                    .ToList();

                if (nuevas.Count > 0)
                {
                    db.Categorias.AddRange(nuevas);
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
