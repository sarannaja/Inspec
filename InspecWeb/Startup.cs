﻿using System;
using System.Net;
using InspecWeb.Controllers;
using InspecWeb.Data;
using InspecWeb.Models;
using InspecWeb.Services;
using InspecWeb.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
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

namespace InspecWeb
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
            // Cors Origins
            // services.AddCors (options => {
            //     options.AddPolicy ("DefaultCorsPolicy",
            //         builder => builder.AllowAnyOrigin ().AllowAnyHeader ().AllowAnyMethod ());
            // });
            // services.AddHostedService<CronJobService>();
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")));

            //<!-- เช็ทพาสเวิร์ด -->
            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 5;
                // options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(3);
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                
                // options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(3);
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            //<!-- เช็ทพาสเวิร์ด
            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            // services.AddIdentity<ApplicationUser, IdentityRole>()
            //.AddEntityFrameworkStores<ApplicationDbContext>()
            //.AddDefaultTokenProviders();

            // services.AddAuthentication ()
            //     .AddIdentityServerJwt ();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    // options.Cookie.Expiration = TimeSpan.FromHours(3);
                    // options.SlidingExpiration = true;
                    // options.Cookie.IsEssential = true;
                    // options.ExpireTimeSpan = TimeSpan.FromHours(3);
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(45);
                })
                .AddIdentityServerJwt();
            // services.AddHttpClient ("testlo", c => {
            //     c.BaseAddress = new Uri ("http://127.0.0.1:3000/");
            //     // Github API versioning
            //     c.DefaultRequestHeaders.Add ("Content-Type", "application/json");
            //     // Github requires a user-agent
            //     // c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            // });


            // เพิ่ม controller ไว้สำหรับทำ cronjob
            services.AddTransient<Controllers.UtinityController, Controllers.UtinityController>();
            services.AddTransient<Controllers.UtinityCheckDateController, Controllers.UtinityCheckDateController>();

            services.AddTransient<Controllers.ExternalOrganizationController, Controllers.ExternalOrganizationController>();
            // services.AddSingleton<Controllers.UtinityController, MyTestHostedService>();
            //end เพิ่ม controller ไว้สำหรับทำ cronjob
            // services.AddControllers();
            services.AddMvc()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .ConfigureApiBehaviorOptions(options =>
                {
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

            // services.Configure<ForwardedHeadersOptions> (options => {
            //     options.ForwardedHeaders =
            //         ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            // });

            // services.Configure<ForwardedHeadersOptions> (options => {
            //     options.ForwardedHeaders =
            //         ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            // });
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardLimit = 2;
                options.KnownProxies.Add(IPAddress.Parse("127.0.10.1"));
                options.ForwardedForHeaderName = "X-Forwarded-For-My-Custom-Header-Name";
            });
            services.AddSingleton<BackgroundService, MyTestHostedService>();
            services.AddHostedService<MyTestHostedService>();
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, Services.MailService>();
            //end mail

            // services.AddSingleton<CronJobService>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            //  services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedProto
            });

            // app.UseCors ("DefaultCorsPolicy");
            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseForwardedHeaders();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseForwardedHeaders();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }


            app.UseIdentityServer();

            //new 
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            //end
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                    // spa.UseProxyToSpaDevelopmentServer("http://127.0.0.1:4200");
                }
            });
        }
    }
}