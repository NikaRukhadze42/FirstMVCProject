using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductsManagementSystem.Interfaces;
using ProductsManagementSystem.Models;
using ProductsManagementSystem.Models.Entities;
using ProductsManagementSystem.Services;

namespace ProductsManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // for get logged in user
            builder.Services.AddHttpContextAccessor();


            //add DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IUserRoleService, UserRoleService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUsersManageService, UsersManageService>();


            // Add Microsoft Identity in builder.Services
            //builder.Services.AddDefaultIdentity<User>().AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


            // Redirect to AccessDenied View
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Home/AccessDenied";
            });


            builder.Services.Configure<IdentityOptions>(x =>
            {
                x.User.RequireUniqueEmail = true;
            });

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
