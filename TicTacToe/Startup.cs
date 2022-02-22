using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicTacToe.Extensions;
using TicTacToe.Interfaces;
using TicTacToe.Services;
using TicTacToe.Services.CRUD;
using TicTacToe.Services.DataContext;
using TicTacToe.Services.Hubs;

namespace TicTacToe
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
            services.AddControllersWithViews();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/SignIn"));
            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddSingleton<IGameInstanceRepository, GameInstanceRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAppAuthenticationService, AppAuthenticationService>();
            services.AddScoped<ITagCrudService, TagCrudService>();
            services.AddScoped<IUserCrudService, UserCrudService>();
            services.AddScoped<IGameCrudService, GameCrudService>();
            services.AddScoped<IGameManager, GameManager>();
            services.AddScoped<ISessionTagCrudService, SessionTagCrudService>();
            services.AddScoped<ITagCrudService, TagCrudService>();
            services.AddAutomapperProfiles();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapFallbackToController("Index", "Home");
                endpoints.MapHub<MainHub>("/MainHub");
                endpoints.MapHub<GameHub>("/GameHub");
            });
        }
    }
}
