using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users_CORE.Configuration;
using Users_CORE.Interfaces;
using Users_CORE.Models;
using Users_CORE.Services;

namespace WebAPI
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
            //services.AddCors(o => o.AddPolicy("CORS_POLICY", builder =>
            //{
            //    builder.WithOrigins("http://localhost:5001/")
            //    .SetIsOriginAllowed(origin => true).AllowAnyMethod()
            //           .AllowAnyHeader();
            //}));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });

            //Se agregan estas l�neas:
            services.AddTransient((ServiceProvider) => BridgeDBConnection<UserModel>.Create(Configuration.GetConnectionString("LocalServer"), CORE.Connection.Models.DbEnum.Sql));
            services.AddTransient((ServiceProvider) => BridgeDBConnection<LoginModel>.Create(Configuration.GetConnectionString("LocalServer"), CORE.Connection.Models.DbEnum.Sql));
            //services.AddTransient((ServiceProvider) => FactorizadorBD<SIFOAModel>.Crear(Configuration.GetConnectionString("SIFOA"), DbEnum.Sql));

            services.AddScoped<IUser, UserService>();
            services.AddScoped<ILogin, LoginService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            //app.UseCors("CORS_POLICY");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
