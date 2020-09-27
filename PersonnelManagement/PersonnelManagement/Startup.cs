using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonnelManagement.Models;
using PersonnelManagement.Udostoverenia;
using PersonnelManagement.Services;
using Pomelo.EntityFrameworkCore.MySql;

namespace PersonnelManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<pmContext>(options => options.UseMySql(
                Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            services.AddDbContext<certContext>(options => options.UseMySql(
                Configuration.GetConnectionString("CertConnection")), ServiceLifetime.Transient);
            //services.AddTransient<IRepository, Repository>();
            services.AddTransient<Repository>();
            services.AddTransient<IdentityService>();
            //services.AddSingleton<Pmmemory>();
            services.AddHangfire(c => c.UseMemoryStorage());
            //services.AddHangfire(_ => _.UseSqlServerStorage(Configuration.GetConnectionString("TasksDb")));
            services.AddDistributedMemoryCache();

            

            services.AddMvc();

            services.AddSession(options =>
            {

            });
            //services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            //services.AddResponseCompression();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseResponseCompression();
            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            Repository repository = app.ApplicationServices.GetService<Repository>();
            //identityService.RemoveObsoleteSessions(repository)
            RecurringJob.AddOrUpdate(() => repository.RemoveObsoleteSessions(), Cron.Minutely());
        }
    }
}
