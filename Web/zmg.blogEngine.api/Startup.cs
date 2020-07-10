using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Microsoft.Extensions.Logging;
using FluentValidation.AspNetCore;
using zmg.blogEngine.services;
using zmg.blogEngine.model;
using zmg.blogEngine.repository;
using zmg.blogEngine.api;
using zmg.blogEngine.api.LoggerAdapter;

namespace zmg.blogEngine.api
{
    public class Startup
    {
        private readonly ILogger _logger;
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region authentication

            //var authority_url = Configuration.GetSection("Authority")["Url"];
            //var client_id = Configuration.GetSection("Authority")["ClientId"];

            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = authority_url;
            //        options.RequireHttpsMetadata = false;
            //        options.ApiName = client_id;
            //    });
            //services.AddHttpClient("OpenIDYPF", c =>
            //{
            //    c.BaseAddress = new Uri(authority_url);
            //});

            #endregion

            services.AddSingleton<INHibernateHelper, NHibernateHelper>();

            services.AddCors(options =>
            {
                //options.AddPolicy("AllowMyOrigin", builder => builder.WithOrigins("http://mysite.com"));
                options.AddPolicy("AllowMyOrigin", builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation()
                .AddMvcOptions(o => o.EnableEndpointRouting = false);

            DependencyInjectionSetup(services);

            #region Swagger Config

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Zemoga Blog Engine API",
                    Version = "v1",
                    Description = "Catalogo HTTP API",
                });
            });

            #endregion
        }

        private void DependencyInjectionSetup(IServiceCollection services)
        {
            _logger.LogInformation("Setting Dependency Objects");
            services.AddTransient<IRepository, RepositoryBase>();
            
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBlogService, BlogService>();
            //services.AddTransient<IUsuariosService, UsuariosService>();
            //services.AddTransient<IUsuariosRepository, UsuariosRepository>();

            //Logger objects
            services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
            //app.UseAuthentication();
            app.UseCors("AllowMyOrigin");
            
            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalago.API V1");
                });

            app.UseHttpsRedirection();
            app.UseMvc(
                routes => {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=BlogV1}/{action=Index}/{id?}");
                });
        }
    }
}
