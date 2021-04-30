using AppAC.Domain.Contracts;
using AppAC.Infrastructure.Data;
using AppAC.Infrastructure.Data.Base;
using AppAC.Infrastructure.Systems;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAC.Infrastructure.WebApi.Filters;
using FluentValidation.AspNetCore;

namespace AppAC.Infrastructure.WebApi
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
            var connectionString = Configuration.GetConnectionString("AppACContext");//obtiene la configuracion del appsettitgs
            var notificationMetadata =
                Configuration.GetSection("NotificationMetadata").
                    Get<NotificationMetadata>();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                //suprime el filtro por defecto q trae asp.net core para enviar los mensajes cuando el modelo no es valido
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddMvc(
                //adicciona el filtro para examinar la validacion de la request
                options =>options.Filters.Add<ValidatorFilter>() 
                ).AddFluentValidation(configuration =>configuration.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddSingleton(notificationMetadata);
            services.AddDbContext<AppACContext>(opt => opt.UseSqlite(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>(); //Crear Instancia por peticion
            services.AddScoped<IPlazoAperturaRepository, PlazoAperturaRepository>(); //Crear Instancia por peticion
            services.AddScoped<ITipoActividadRepository, TipoActividadRepository>();
            services.AddScoped<IActividadRepository, ActividadRepository>(); 
            services.AddScoped<IPlanAccionRepository, PlanAccionRepository>();
            services.AddScoped<IItemPlanRepository, ItemPlanRepository>();
            services.AddScoped<IDbContext, AppACContext>(); //Crear Instancia por peticion
            services.AddScoped<IMailServer, MailServer>(); //Crear Instancia por peticion
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AppAC.Infrastructure.WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppAC.Infrastructure.WebApi v1"));
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
