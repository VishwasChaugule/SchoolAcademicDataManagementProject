using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using SchoolAcademicDataManagement.Data;
using SchoolAcademicDataManagement.Filters;
using SchoolAcademicDataManagement.Services;

namespace SchoolAcademicDataManagement
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<SchoolDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddHttpClient("SchoolAcademicDataApi", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5217");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddScoped<AdminAuthorizationFilter>();
            services.AddScoped<IUserService, UserService>();
            services.AddSession();

            /*services.AddMvc(options =>
            {
                options.Filters.Add(typeof(AdminAuthorizationFilter));
            });*/

            services.AddScoped<IDataSeeder, DataSeeder>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Migrate database to the latest version
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<SchoolDBContext>();
                dbContext.Database.Migrate();
            }

            // Seed data
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dataSeeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
                dataSeeder.SeedData();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
