using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using Microsoft.AspNetCore.Mvc.Controllers;
using SimpleInjector.Integration.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using SimpleInjector.Lifestyles;
using MongoDbWithAspNetCore.Data;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace MongoDbWithAspNetCore
{
    public class Startup
    {
        private Container _container;
        public Startup(IHostingEnvironment env)
        {
            _container = new Container();
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddSingleton<IControllerActivator>(
            new SimpleInjectorControllerActivator(_container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(_container));
            services.AddMvc();
            services.UseSimpleInjectorAspNetRequestScoping(_container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            InitializeContainer(app);

            _container.Verify();

            app.Use(async (context, next) => {
                await next();
            });
            app.UseMvc();
        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Add application presentation components:
            _container.RegisterMvcControllers(app);
            _container.RegisterMvcViewComponents(app);

            // Add application services. For instance:
            _container.Register<ICustomerRepository, CustomerRepository>(Lifestyle.Scoped);
        }
    }
}
