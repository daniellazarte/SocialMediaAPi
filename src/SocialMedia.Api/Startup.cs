using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMedia.Api.Controllers;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Services;
using SocialMedia.Infraestructure.Filters;
using SocialMedia.Infraestructure.Interfaces;
using SocialMedia.Infraestructure.Repositories;
using SocialMedia.Infraestructure.Services;
using SocialMedia.Infrastructure.Data;
using System;

namespace SocialMedia.Api
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
            //Configurar y registrar el Automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Evitar referencias circulares TIP
            //COnfigurar las Custom Exceptions
            services.AddControllers( options => 
            {
                options.Filters.Add<GlobalExceptionFilter>();


            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

            })

             //Si deseas desactivar la validacion automatica de .Net Core CCon esta configuracion lo logras
            .ConfigureApiBehaviorOptions(options =>
            {
                //options.SuppressModelStateInvalidFilter = true;

            });
            //fin de la configuracion

            services.AddControllers();

            //Registrar la Conexion
            services.AddDbContext<SocialMediaContext>(Options =>
                Options.UseSqlServer(Configuration.GetConnectionString("SocialMedia"))
            );

            //COnfigurar Lectura de Appsettings en PAinationOptions
            services.Configure<PaginationOptions>(Configuration.GetSection("Pagination"));

            //Registrando el Servicio IPostService
            services.AddTransient<IPostService, PostService>();
            //services.AddTransient<IPostRepository, PostRepository>();
            //services.AddTransient<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IRepository<>),typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //Registrar el Servicio de IURIservice
            services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteuri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteuri);
            });

            //Registrar un ActionFilter Personalizado
            services.AddMvc(Options =>
            {
                Options.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(Options =>
            {
                Options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
