using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyPartyCore.BL;
using MyPartyCore.DAL;
using AutoMapper;
using MyPartyCore.Middleware;
using FluentValidation.AspNetCore;
using MyPartyCore.ConfigurationProviders;
using MyPartyCore.Models;
using Microsoft.AspNetCore.Identity;
using MyPartyCore.AuthorizationPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MyPartyCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddDatabaseConfiguration("Server=localhost\\SQLEXPRESS;Database=MyPartiesEF;Trusted_Connection=True;");
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddTransient<IConfiguration>(provider => Configuration);


            //string connectionString = Configuration.GetConnectionString("MyPartyDatabase");
            //services.AddTransient<IPartyRepository>(x => new ADOPartyRepository(connectionString));
            //services.AddTransient<IParticipantsRepository>(x => new ADOParticipantsRepository(connectionString));

            services.AddDbContext<MyPartyContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MyPartyDatabaseEF")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<MyPartyContext>();

            services.AddHttpContextAccessor();

            services.AddTransient<IPartyService, PartyService>();

            services.AddAutoMapper(typeof(Mappings.MappingProfile));

            services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
            services.AddSingleton<IAuthorizationHandler, MinimumAgePartyHandler>();
            services.AddSingleton<IAuthorizationHandler, PartyAuthorizationCrudHandler>();


            services.AddAuthorization(options =>
            {
                options.AddPolicy("Over18", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new MinimumAgeRequirement(18));
                });
                options.AddPolicy("ReadPartyOver18", policy =>
                    policy.Requirements.Add(new MinimumAgeRequirement(18)));
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
            {
                options.LoginPath = new PathString("/Account/Login");
            });

            services.AddSession();

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fv =>
                {
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseMiddlewareTrace();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMiddlewareExport();

            app.UseAuthentication();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
