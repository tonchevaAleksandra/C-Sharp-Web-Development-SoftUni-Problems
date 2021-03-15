using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AspNetMiddlewareDemo
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
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.Map("/softuni", app => // Map has own configurations
            {
                app.UseWelcomePage();
                //app.Run(async (request) =>
                //{
                //    await request.Response.WriteAsync("I'm last middleware configured for Softuni path !");
                //});
            });

            app.UseMiddleware<EveryTwoSecondsMiddleware>();

            //app.Use(async (context, next) =>
            //{

            //    //context.Items
            //    //if (context.Request.Cookies.ContainsKey())
            //    //{
                    
            //    //}
            //    if (context.Request.Scheme != "https") 
            //    {
            //        context.Response.Headers["Location"] = "https";
            //    }

            //    //if (context.Request.Path.Value.EndsWith(".css"))
            //    //{
            //    //    context.Response.Body.
            //    //}
           

            //});

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("I'm the second middleware!");
                await next();
                await context.Response.WriteAsync("I'm the second middleware! My last execution");
            });

            app.Run(async (request) =>
            {
                await request.Response.WriteAsync("I am the one and only!");
            });// Run will be the last - Run does not invoke next

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("I'm the third middleware!");
                //await next();
                await context.Response.WriteAsync("I'm the third middleware! My last execution");
            });
        }
    }

    public class EveryTwoSecondsMiddleware
    {
        private RequestDelegate next;

        public EveryTwoSecondsMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync("Hello!!! I'm first middleware!");
            if (DateTime.UtcNow.Second % 2 == 0)
            {
                await next(context);
            }

            await context.Response.WriteAsync("I'm the first middleware! My last execution");
        }
    }
}
