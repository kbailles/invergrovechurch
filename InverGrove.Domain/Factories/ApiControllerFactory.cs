using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Castle.Windsor;
using InverGrove.Domain.Exceptions;

namespace InverGrove.Domain.Factories
{        
    /// <summary>
    /// Used to resolve the ApiController for WebApi
    /// </summary>
    public class ApiControllerFactory : IHttpControllerActivator
    {
        private readonly IWindsorContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiControllerFactory"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public ApiControllerFactory(IWindsorContainer container = null)
        {
            this.container = container ?? IocFactory.Instance;
        }

        /// <summary>
        /// Creates an <see cref="T:System.Web.Http.Controllers.IHttpController" /> object.
        /// </summary>
        /// <param name="request">The message request.</param>
        /// <param name="controllerDescriptor">The HTTP controller descriptor.</param>
        /// <param name="controllerType">The type of the controller.</param>
        /// <returns>
        /// An <see cref="T:System.Web.Http.Controllers.IHttpController" /> object.
        /// </returns>
        /// <remarks>Not checking the controllerDescriptor as we don't care about its state.</remarks>
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            if (request == null)
            {
                throw new ParameterNullException("request");
            }
            if (controllerType == null)
            {
                throw new ParameterNullException("controllerType");
            }

            var controllerName = controllerType.Name.ToLower();
            var controller = this.container.Resolve<IHttpController>(controllerName);

            // Adds the given resource to a list of resources that will be disposed
            // by a host once the request is disposed.
            request.RegisterForDispose(new Release(() => this.container.Release(controller)));

            return controller;
        }

        private class Release : IDisposable
        {
            private readonly Action release;

            public Release(Action release)
            {
                this.release = release;
            }

            public void Dispose()
            {
                this.release();
            }
        }
    }
}
