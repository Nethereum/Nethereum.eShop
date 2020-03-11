using Ardalis.ListStartupServices;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Nethereum.eShop.ApplicationCore.Interfaces;
using Nethereum.eShop.ApplicationCore.Services;
using Nethereum.eShop.DbFactory;
using Nethereum.eShop.EntityFramework.Identity;
using Nethereum.eShop.Infrastructure.Cache;
using Nethereum.eShop.Infrastructure.Identity;
using Nethereum.eShop.Infrastructure.Logging;
using Nethereum.eShop.Infrastructure.Services;
using Nethereum.eShop.Web.Interfaces;
using Nethereum.eShop.Web.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

namespace Nethereum.eShop.Web
{
    public class Startup
    {
        private IServiceCollection _services;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureProductionServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            IEShopDbBootstrapper dbBootstrapper = EShopDbBootstrapper.CreateDbBootstrapper(Configuration);
            services.AddSingleton(dbBootstrapper);
            dbBootstrapper.AddDbContext(services, Configuration);

            IEShopIdentityDbBootstrapper identityDbBootstrapper = EShopDbBootstrapper.CreateAppIdentityDbBootstrapper(Configuration);
            services.AddSingleton(identityDbBootstrapper);
            identityDbBootstrapper.AddDbContext(services, Configuration);

            ConfigureServices(services, dbBootstrapper);
        }

        public void ConfigureTestingServices(IServiceCollection services)
        {
            Configuration["CatalogDbProvider"] = "InMemory";
            Configuration["AppIdentityDbProvider"] = "InMemory";
            ConfigureProductionServices(services);
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(
            IServiceCollection services, IEShopDbBootstrapper dbBootstrapper)
        {
            ConfigureCookieSettings(services);

            CreateIdentityIfNotCreated(services);

            services.AddMediatR(typeof(BasketViewModelService).Assembly);

            dbBootstrapper.AddQueries(services, Configuration);
            dbBootstrapper.AddRepositories(services, Configuration);

            services.AddScoped(typeof(IAsyncCache<>), typeof(GeneralCache<>));
            services.AddScoped<ICatalogViewModelService, CachedCatalogViewModelService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IBasketViewModelService, BasketViewModelService>();
            services.AddScoped<IQuoteService, QuoteService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IRulesEngineService, RulesEngineService>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IRuleTreeCache, RuleTreeCache>();
            services.AddScoped<CatalogViewModelService>();
            services.AddScoped<ICatalogItemViewModelService, CatalogItemViewModelService>();
            services.Configure<CatalogSettings>(Configuration);

            var catalogSettings = Configuration.Get<CatalogSettings>();
            services.AddSingleton(catalogSettings);

            services.AddSingleton<IUriComposer>(new UriComposer(catalogSettings));

            dbBootstrapper.AddSeeders(services, Configuration);

            var rulesEngineSettings = Configuration.Get<RulesEngineSettings>();

            services.AddSingleton<IRulesEngineInitializer>(new RulesEngineInitializer(rulesEngineSettings));

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddTransient<IEmailSender, EmailSender>();

            // Add memory cache services
            services.AddMemoryCache();

            services.AddRouting(options =>
            {
                // Replace the type and the name used to refer to it with your own
                // IOutboundParameterTransformer implementation
                options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
            });

            services.AddMvc(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(
                         new SlugifyParameterTransformer()));

            });
            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizePage("/Basket/Checkout");
            });
            services.AddControllersWithViews();
            services.AddControllers();

            services.AddHttpContextAccessor();
            
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1"}));

            services.AddHealthChecks();

            services.Configure<ServiceConfig>(config =>
            {
                config.Services = new List<ServiceDescriptor>(services);

                config.Path = "/allservices";
            });

            _services = services; // used to debug registered services
        }

        private static void CreateIdentityIfNotCreated(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var existingUserManager = scope.ServiceProvider
                    .GetService<UserManager<ApplicationUser>>();
                if(existingUserManager == null)
                {
                    services.AddIdentity<ApplicationUser, IdentityRole>()
                        .AddDefaultUI()
                        .AddEntityFrameworkStores<AppIdentityDbContext>()
                                        .AddDefaultTokenProviders();
                }
            }
        }

        private static void ConfigureCookieSettings(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.Cookie = new CookieBuilder
                {
                    IsEssential = true // required for auth to work without explicit user consent; adjust to suit your privacy policy
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHealthChecks("/health",
                new HealthCheckOptions
                {
                    ResponseWriter = async (context, report) =>
                    {
                        var result = JsonConvert.SerializeObject(
                            new
                            {
                                status = report.Status.ToString(),
                                errors = report.Entries.Select(e => new
                                {
                                    key = e.Key,
                                    value = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                                })
                            });
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseShowAllServicesMiddleware();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseRouting();
            
            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller:slugify=Home}/{action:slugify=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapHealthChecks("home_page_health_check");
                endpoints.MapHealthChecks("api_health_check");
            });
        }
    }

}
