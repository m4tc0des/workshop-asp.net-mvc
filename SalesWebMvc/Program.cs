using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMvc.Data;
using SalesWebMvc.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace SalesWebMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configura a string de conexão
            var connectionString = builder.Configuration.GetConnectionString("SalesWebMvcContext")
                ?? throw new InvalidOperationException("Connection string 'SalesWebMvcContext' not found.");
            var enUS = new CultureInfo("en-US");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(enUS),
                SupportedCultures = new List<CultureInfo> { enUS },
                SupportedUICultures = new List<CultureInfo> { enUS },
                
            };
            

            // Configura o DbContext com MySQL
            builder.Services.AddDbContext<SalesWebMvcContext>(options =>
                options.UseMySql(
                    connectionString,
                    new MySqlServerVersion(new Version(8, 0, 11)),
                    b => b.MigrationsAssembly("SalesWebMvc")
                )
            );

            // Injeta os serviços ANTES de builder.Build()
            builder.Services.AddScoped<SeedingService>();
            builder.Services.AddScoped<SellerService>();
            builder.Services.AddScoped<DepartmentService>();

            // Adiciona suporte a controllers com views
            builder.Services.AddControllersWithViews();

            // Constrói o app
            var app = builder.Build();
            app.UseRequestLocalization(localizationOptions);

            // Executa o seeding (só insere dados se o banco estiver vazio)
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seedingService = services.GetRequiredService<SeedingService>();
                seedingService.Seed();
            }

            // Configura o pipeline de requisição HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}