using BL.manager;
using DAL.context;
using DAL.repository;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presentation_layer;

namespace Restaurant
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
             
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<Restaurantcontext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("sqlserverConnection")));


            builder.Services.AddScoped(typeof(igenericrepository<>), typeof(genericrepository<>));
            builder.Services.AddScoped<iUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductmanager, productmanger>();
            builder.Services.AddScoped<Icategorymanager,categorymanager>();
            builder.Services.AddScoped<IOrdermanager,Ordermanager>();
            builder.Services.AddScoped<iCustomermanager, Customermanager>();
            builder.Services.AddScoped<iProductOrdermanager, ProductOrdermanager>();
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<Restaurantcontext>();

            builder.Services.AddScoped<productrepository>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var config = services.GetRequiredService<IConfiguration>();

                DbInitializer.SeedAdminAsync(services, config)
                             .GetAwaiter()
                             .GetResult();
            }
            app.Run();
        }
    }
}
