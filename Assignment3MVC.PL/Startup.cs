using Assignment3MVC.BLL.Interfaces;
using Assignment3MVC.BLL.Repositories;
using Assignment3MVC.DAL.Contexts;
using Assignment3MVC.DAL.Entities;  
using Assignment3MVC.PL.Mapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3MVC.PL
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

            services.AddDbContext<ProjectMVCDbContext>(options =>
            {
                //options.UseSqlServer("Server = .; Database = ProjectMVCDb; Trusted_Connection = true;");
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //services.AddSingleton<IDepartmentRepository, DepartmentRepository>(); /// life time object when user open application over App
            //services.AddTransient<IDepartmentRepository, DepartmentRepository>(); ///  life time object per operation
            services.AddScoped<IDepartmentRepository, DepartmentRepository>(); /// life time object per request
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
            services.AddAutoMapper(D => D.AddProfile(new DepartmentProfile()));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>  
            {
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6; //min
            })
                .AddEntityFrameworkStores<ProjectMVCDbContext>()
                 .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);


            //services.AddScoped<UserManager<ApplicationUser> , UserManager<ApplicationUser>>();  
            //services.AddScoped<SignInManager<ApplicationUser>, SignInManager<ApplicationUser>>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) // Allow the userManger and SignInManger Scoped
                .AddCookie(options =>
                {
                    options.LoginPath = "Account/Login";
                    options.AccessDeniedPath = "Home/Error";
                });

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

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Register}/{id?}");
            });
        }
    }
}
