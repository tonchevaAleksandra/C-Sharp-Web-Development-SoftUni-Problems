using AspNetAppForTestingRazor.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetAppForTestingRazor.Filters;
using AspNetAppForTestingRazor.Services;

namespace AspNetAppForTestingRazor
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews(configure => //global registration of a filter => this filter will be invoke on every action
            {
                configure.Filters.Add(new AddHeaderActionFilterAttribute());                configure.Filters.Add(new MyAuthFilter());          
                configure.Filters.Add(new MyExceptionFilter());                configure.Filters.Add(new MyResourceFilter());                configure.Filters.Add(new MyResultFilterAttribute());
                //configure.Filters.Add(typeof(AddHeaderActionFilter));

            });
            services.AddRazorPages();

            // Singleton
            //services.AddSingleton<IInstanceCounter, InstanceCounter>();// One instance till the program is running

            // Scoped - every request makes one instance
            //services.AddScoped<IInstanceCounter, InstanceCounter>();

            // Transient  // Transient is the most used option, makes new instance on every invoke
           
            services.AddTransient<IInstanceCounter, InstanceCounter>();

            //services.AddSingleton<AddHeaderActionFilterAttribute>(); => if we need to use DI and ServiceFilter(typeof(AddHeaderActionFilterAttribute))] we need to register not just the service we inject in the constructor but also the Filter-Attribute 
            services.AddTransient<IShortStringService, ShortStringService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseStatusCodePagesWithRedirects("/Home/StatusCodeError?errorCode={0}");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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
                endpoints.MapRazorPages();
            });
        }
    }
}
