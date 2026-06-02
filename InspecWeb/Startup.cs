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
            services.AddCors(options =>
                {
                    options.AddPolicy("AngularClient", builder =>
                    {
                        builder.WithOrigins("https://inspection.opm.go.th")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
                });
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
            // services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //     .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            //     {
            //         // options.Cookie.Expiration = TimeSpan.FromHours(3);
            //         // options.SlidingExpiration = true;
            //         // options.Cookie.IsEssential = true;
            //         // options.ExpireTimeSpan = TimeSpan.FromHours(3);
            //         options.ExpireTimeSpan = TimeSpan.FromMinutes(45);
            //         options.Cookie.HttpOnly = true;
            //         options.Cookie.SameSite = SameSiteMode.None;
            //         options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            //     })
            //     .AddIdentityServerJwt();

            services.AddAuthentication()
                .AddCookie(options =>
                    {
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(45);
                        options.Cookie.HttpOnly = true;
                        // 6. Cookie with SameSite Attribute None
                        options.Cookie.SameSite = SameSiteMode.Lax;

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
                // 6. Cookie No HttpOnly Flag — force HttpOnly on all cookies
                options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
                options.Secure = CookieSecurePolicy.Always;
                // 7. Cookie with SameSite None — upgrade any SameSite=None to Lax
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
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

            // ❗ แนะนำ: จำกัด CORS แทน AllowAnyOrigin
            app.UseCors("AngularClient");
            // app.UseCors(policy =>
            //     policy.WithOrigins("https://inspection.opm.go.th")
            //           .AllowAnyMethod()
            //           .AllowAnyHeader()
            // );

            // ✅ Global Security Headers (OWASP ZAP mitigations)
            app.Use(async (context, next) =>
            {
                // Prevent MIME-type sniffing
                context.Response.Headers["X-Content-Type-Options"] = "nosniff";

                // Clickjacking protection
                context.Response.Headers["X-Frame-Options"] = "DENY";

                // Disable legacy XSS auditor (modern browsers ignore it; set to 0 per OWASP)
                context.Response.Headers["X-XSS-Protection"] = "0";

                // Referrer leakage reduction
                context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";

                // Remove server-identifying headers
                context.Response.Headers.Remove("X-Powered-By");
                context.Response.Headers.Remove("Server");

                // Content Security Policy
                if (env.IsDevelopment())
                {
                    // Development: allow unsafe-eval for Angular CLI hot-reload
                    context.Response.Headers["Content-Security-Policy"] =
                        "default-src 'self'; " +
                        "script-src 'self' 'unsafe-eval'; " +
                        "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com https://cdnjs.cloudflare.com; " +
                        // data: required — flaticon/ionicons load icon fonts as base64 data URIs
                        "font-src 'self' data: https://fonts.gstatic.com https://cdnjs.cloudflare.com; " +
                        "img-src 'self' data: blob:; " +
                        "connect-src 'self' https://inspection.opm.go.th ws://localhost:4200; " +
                        "object-src 'none'; " +
                        "base-uri 'self'; " +
                        // 'self' allows IdentityServer login/consent pages to be framed by the same origin
                        "frame-ancestors 'self';";
                }
                else
                {
                    // Production: strict CSP — no unsafe-inline/eval in script-src
                    // Note: 'unsafe-inline' in style-src is required by Angular Material
                    //       which injects component styles at runtime via <style> tags.
                    context.Response.Headers["Content-Security-Policy"] =
                        "default-src 'self'; " +
                        "script-src 'self' https://ajax.aspnetcdn.com; " +
                        "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com https://cdnjs.cloudflare.com; " +
                        // data: required — flaticon/ionicons load icon fonts as base64 data URIs
                        "font-src 'self' data: https://fonts.gstatic.com https://cdnjs.cloudflare.com; " +
                        "img-src 'self' data: blob:; " +
                        "connect-src 'self' https://inspection.opm.go.th wss://inspection.opm.go.th; " +
                        "object-src 'none'; " +
                        // 'self' allows IdentityServer login/consent pages to be framed by the same origin
                        "frame-ancestors 'self'; " +
                        "base-uri 'self'; " +
                        "form-action 'self';";
                }
                await next();
            });

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
                app.UseHsts(); // ✅ เปิด HSTS production
            }

            

            if (!env.IsDevelopment()) 
            {
                app.UseSpaStaticFiles();
            }

            app.UseIdentityServer();   // OK ตรงนี้

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();

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