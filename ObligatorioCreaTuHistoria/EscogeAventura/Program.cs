using EscogeAventura.LogicaAccesoDatos;
using EscogeAventura.LogicaAccesoDatos.Repos;
using EscogeAventura.LogicaAplicacion.CasosUso.CURol;
using EscogeAventura.LogicaAplicacion.CasosUso.CUUsuario;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICURol;
using EscogeAventura.LogicaAplicacion.ICasosUso.ICUUsuario;
using EscogeAventura.LogicaNegocios.IRepositorios;
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



            //ID - CU
            builder.Services.AddScoped<ICUAltaUsuario, CUAltaUsuario>();
            builder.Services.AddScoped<ICUVerifExistencia, CUVerifExistencia>();
            builder.Services.AddScoped<ICUObtenerRol, CUObtenerRol>();


            var app = builder.Build();

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
