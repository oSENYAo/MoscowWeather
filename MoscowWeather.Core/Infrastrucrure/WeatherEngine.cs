using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoscowWeather.Core.Infrastrucrure;

namespace Mithra.Core.Infrastructure
{
    public class WeatherEngine : IEngine
    {

        #region Properties

        /// <summary>
        /// Gets or sets service provider
        /// </summary>
        private IServiceProvider _serviceProvider { get; set; }

        #endregion

        #region Utilities

        /// <summary>
        /// Get IServiceProvider
        /// </summary>
        /// <returns>IServiceProvider</returns>
        protected IServiceProvider GetServiceProvider()
        {
            if (ServiceProvider == null)
                return null;
            var accessor = ServiceProvider?.GetService<IHttpContextAccessor>();
            var context = accessor?.HttpContext;
            return context?.RequestServices ?? ServiceProvider;
        }

        /// <summary>
        /// Register dependencies
        /// </summary>
        /// <param name="services">services</param>
        /// <param name="dependencyRegistrarInstances">Dependency Registrar Instances</param>
        public virtual void RegisterDependencies(IServiceCollection services, List<IDependencyRegistrar> dependencyRegistrarInstances = null)
        {
            //register engine
            services.AddSingleton<IEngine>(this);

            if (dependencyRegistrarInstances != null)
            {
                //register all provided dependencies
                foreach (var dependencyRegistrar in dependencyRegistrarInstances)
                    dependencyRegistrar.Register(services);
            }
        }


        #endregion

        #region Methods
         public void ConfigureRequestPipeline(IApplicationBuilder application)
        {
            _serviceProvider = application.ApplicationServices;

        }

        public void ConfigureRequestPipeline(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

        }

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <typeparam name="T">Type of resolved service</typeparam>
        /// <returns>Resolved service</returns>
        public T Resolve<T>() where T : class
        {
            return (T)Resolve(typeof(T));
        }

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <param name="type">Type of resolved service</param>
        /// <returns>Resolved service</returns>
        public object Resolve(Type type)
        {
            var sp = GetServiceProvider();
            if (sp == null)
                return null;

            return sp.GetService(type);
        }

        /// <summary>
        /// Resolve dependencies
        /// </summary>
        /// <typeparam name="T">Type of resolved services</typeparam>
        /// <returns>Collection of resolved services</returns>
        public virtual IEnumerable<T> ResolveAll<T>()
        {
            return (IEnumerable<T>)GetServiceProvider().GetServices(typeof(T));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Service provider
        /// </summary>
        public virtual IServiceProvider ServiceProvider => _serviceProvider;

        #endregion
    }
}