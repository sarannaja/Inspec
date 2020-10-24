using System;
using InspecWeb.Controllers;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.Services;
using InspecWeb.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace InspecWeb {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            // Cors Origins
            // services.AddCors (options => {
            //     options.AddPolicy ("DefaultCorsPolicy",
            //         builder => builder.AllowAnyOrigin ().AllowAnyHeader ().AllowAnyMethod ());
            // });
            // services.AddHostedService<CronJobService>();
            services.AddDbContext<ApplicationDbContext> (options =>
                options.UseSqlServer (
                    Configuration.GetConnectionString ("DefaultConnection")));

            //<!-- เช็ทพาสเวิร์ด -->
            services.AddDefaultIdentity<ApplicationUser> (options => {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext> ().AddDefaultTokenProviders ();
            //<!-- เช็ทพาสเวิร์ด
            services.AddIdentityServer ()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext> ();

            // services.AddIdentity<ApplicationUser, IdentityRole>()
            //.AddEntityFrameworkStores<ApplicationDbContext>()
            //.AddDefaultTokenProviders();

            // services.AddAuthentication ()
            //     .AddIdentityServerJwt ();
            services.AddAuthentication (CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie (CookieAuthenticationDefaults.AuthenticationScheme, options => {
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = new TimeSpan (0, 1, 0);
                })
                .AddIdentityServerJwt ();
            services.AddHttpClient ("testlo", c => {
                c.BaseAddress = new Uri ("http://127.0.0.1:3000/");
                // Github API versioning
                c.DefaultRequestHeaders.Add ("Content-Type", "application/json");
                // Github requires a user-agent
                // c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            });

            // เพิ่ม controller ไว้สำหรับทำ cronjob
            services.AddTransient<Controllers.UtinityController, Controllers.UtinityController> ();
            // services.AddSingleton<Controllers.UtinityController, MyTestHostedService>();
            //end เพิ่ม controller ไว้สำหรับทำ cronjob
            // services.AddControllers();
            services.AddMvc ()
                .AddNewtonsoftJson (options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .ConfigureApiBehaviorOptions (options => {
                    options.SuppressConsumesConstraintForFormFileParameters = true;
                    options.SuppressInferBindingSourcesForParameters = true;
                    options.SuppressModelStateInvalidFilter = true;
                    options.SuppressMapClientErrors = true;
                });

            //mail
            // var emailConfig = Configuration
            //     .GetSection("EmailConfiguration")
            //     .Get<EmailConfiguration>();
            // services.AddSingleton(emailConfig);
            // services.AddTransient<IEmailSender, EmailSender>();
            //end mail
            //mail
            services.AddSingleton<IHostedService, MyTestHostedService> ();
            services.AddHostedService<MyTestHostedService> ();
            services.Configure<MailSettings> (Configuration.GetSection ("MailSettings"));
            services.AddTransient<IMailService, Services.MailService> ();
            //end mail

            // services.AddSingleton<CronJobService>();
            services.AddControllersWithViews ();
            services.AddRazorPages ();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles (configuration => {
                configuration.RootPath = "ClientApp/dist";
            });
            //  services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {

            // app.UseCors ("DefaultCorsPolicy");
            app.UseCors (x => x
                .AllowAnyOrigin ()
                .AllowAnyMethod ()
                .AllowAnyHeader ()
            );

            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
                app.UseDatabaseErrorPage ();
            } else {
                app.UseExceptionHandler ("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();
            app.UseStaticFiles ();
            if (!env.IsDevelopment ()) {
                app.UseSpaStaticFiles ();
            }

            app.UseRouting ();

            app.UseAuthentication ();
            app.UseIdentityServer ();
            app.UseAuthorization ();
            app.UseEndpoints (endpoints => {
                endpoints.MapControllerRoute (
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages ();
            });

            app.UseSpa (spa => {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment ()) {
                    spa.UseAngularCliServer (npmScript: "start");
                }
            });
        }
    }
}