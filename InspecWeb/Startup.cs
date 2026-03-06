using System;
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
using Microsoft.AspNetCore.Http;

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

            services.AddCors();

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
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SameSite = SameSiteMode.None;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
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

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
                options.Secure = CookieSecurePolicy.Always;
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
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
            // ✅ Forwarded headers (reverse proxy / IIS / nginx)
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedProto
            });

            // ✅ Global Security Headers
            app.Use(async (context, next) =>
            {
                context.Response.Headers["X-Content-Type-Options"] = "nosniff";
                // context.Response.Headers["X-Frame-Options"] = "DENY";
                context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
                context.Response.Headers["X-XSS-Protection"] = "0";

                if (env.IsDevelopment())
                {
                    context.Response.Headers["Content-Security-Policy"] =
                        "default-src 'self'; " +
                        "script-src 'self' 'unsafe-eval'; " +
                        "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com https://cdnjs.cloudflare.com; " +
                        "font-src 'self' https://fonts.gstatic.com https://cdnjs.cloudflare.com data:; " +
                        "img-src 'self' data: blob:; " +
                        "connect-src 'self' ws: https:;";
                }
                else
                {
                    context.Response.Headers["Content-Security-Policy"] =
                        "default-src 'self'; " +
                        "script-src 'self' 'unsafe-inline' 'unsafe-eval' https://ajax.aspnetcdn.com;; " +
                        "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com https://cdnjs.cloudflare.com; " +
                        "font-src 'self' https://fonts.gstatic.com https://cdnjs.cloudflare.com; " +
                        "img-src 'self' data: blob:; " +
                        "connect-src 'self' https://inspection.opm.go.th https: ws: wss:;";
                        // "object-src 'none'; " +
                        // "base-uri 'self';";
                }

                context.Response.Headers.Remove("X-Powered-By");
                await next();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts(); // ✅ เปิด HSTS production
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            // ❗ แนะนำ: จำกัด CORS แทน AllowAnyOrigin
            app.UseCors(policy =>
                policy.WithOrigins("https://inspection.opm.go.th")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
            );

            app.UseCookiePolicy();

            app.UseIdentityServer();   // OK ตรงนี้
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}