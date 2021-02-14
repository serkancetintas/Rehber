using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Setur.Services.Contact.Application;
using Setur.Services.Contact.Core.Repositories;
using Setur.Services.Contact.Infrastructure;
using Setur.Services.Contact.Infrastructure.Mongo;
using Setur.Services.Contact.Infrastructure.Mongo.Repositories;

namespace Setur.Services.Contact.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private static void AddServiceDependency(IServiceCollection services)
        {
            var factory = new Open.Serialization.Json.Newtonsoft.JsonSerializerFactory(new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { new StringEnumConverter(true) }
            });
            var jsonSerializer = factory.GetSerializer();

            if (jsonSerializer.GetType().Namespace?.Contains("Newtonsoft") == true)
            {
                services.Configure<KestrelServerOptions>(o => o.AllowSynchronousIO = true);
                services.Configure<IISServerOptions>(o => o.AllowSynchronousIO = true);
            }

            services.AddSingleton(jsonSerializer);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IContactRepository, ContactRepository>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            ConfigureDbSettings(services);
            AddServiceDependency(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseRouting();

            app.UseInfrastructure();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                endpoints.MapControllers();
            });
        }

        public virtual void ConfigureDbSettings(IServiceCollection services)
        {
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));
            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                            serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            services.AddScoped(typeof(IMongoRepository<,>), typeof(MongoRepository<,>));
        }
    }
}
