using Exame_Online.Data.Contexto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static Exame_Online.Data.Entidades.IdentyModel;

namespace Exame_Online
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var conexao = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<Contexto>(options => options.UseSqlServer(conexao));
            services.AddControllersWithViews();
            services.AddIdentity<Usuario, Perfil>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;


            }).AddEntityFrameworkStores<Contexto>()
            .AddDefaultTokenProviders();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireElevatedRights", policy => policy.RequireRole("Candidato", "Administrador"));
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings  
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Utanga/Login"; // Se o LoginPath não for definido aqui, o ASP.NET Core irá usar como padrão / Utanga / Login
                options.LogoutPath = "/Utanga/Logout";// Se o LoginPath não for definido aqui, o ASP.NET Core irá usar como padrão / Utanga / Logout
                options.AccessDeniedPath = "/Utanga/AcessoNevado";  // Se o AccessDeniedPath não for definido aqui, o ASP.NET Core será padronizado como / Utanga/ AcessoNegado
                options.SlidingExpiration = true;
            });
   



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Contexto ctx, UserManager<Usuario> userManager, RoleManager<Perfil> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //Quem és?
            app.UseAuthentication();

            //Permissões?  
            app.UseAuthorization();
            DataSeed.SeedData(ctx, userManager, roleManager);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Utanga}/{action=Login}/{id?}");
            });
        }
    }
}
