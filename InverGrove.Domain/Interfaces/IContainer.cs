using System;
using System.Collections;
using Castle.Core;
using Castle.MicroKernel;


namespace InverGrove.Domain.Interfaces
{
    /// <summary>
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="classType">Type of the class.</param>
        /// <remarks>This method is thread-safe.</remarks>
        void AddComponent(string key, Type classType);

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="classType">Type of the class.</param>
        /// <remarks>This method is thread-safe.</remarks>
        void AddComponent(string key, Type serviceType, Type classType);

        /// <summary>
        /// Adds the component with lifestyle.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="lifestyle">The lifestyle.</param>
        /// <remarks>This method is thread-safe.</remarks>
        void AddComponentWithLifestyle(string key, Type classType, LifestyleType lifestyle);

        /// <summary>
        /// Adds the component with lifestyle.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="lifestyle">The lifestyle.</param>
        /// <remarks>This method is thread-safe.</remarks>
        void AddComponentWithLifestyle(string key, Type serviceType, Type classType, LifestyleType lifestyle);

        /// <summary>
        /// Adds the component with properties.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="extendedProperties">The extended properties.</param>
        /// <remarks>This method is thread-safe.</remarks>
        void AddComponentWithProperties(string key, Type classType, IDictionary extendedProperties);

        /// <summary>
        /// Adds the component with properties.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="extendedProperties">The extended properties.</param>
        /// <remarks>This method is thread-safe.</remarks>
        void AddComponentWithProperties(string key, Type serviceType, Type classType, IDictionary extendedProperties);

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <remarks>This method is thread-safe.</remarks>
        void AddComponent<T>()
            where T : class;

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <remarks>This method is thread-safe.</remarks>
        void AddComponent<T>(string key)
            where T : class;

        /// <summary>
        /// Adds the component with lifestyle.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lifestyle">The lifestyle.</param>
        void AddComponentWithLifestyle<T>(LifestyleType lifestyle)
            where T : class;

        /// <summary>
        /// Adds the component with lifestyle.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="lifestyle">The lifestyle.</param>
        /// <remarks>This method is thread-safe.</remarks>
        void AddComponentWithLifestyle<T>(string key, LifestyleType lifestyle)
            where T : class;

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TU">The type of the U.</typeparam>
        /// <remarks>
        /// This method is thread-safe.
        /// </remarks>
        void AddComponent<T, TU>()
            where TU : class,
                T
            where T : class;

        /// <summary>
        /// Adds the component.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TU">The type of the U.</typeparam>
        /// <param name="key">The key.</param>
        /// <remarks>
        /// This method is thread-safe.
        /// </remarks>
        void AddComponent<T, TU>(string key)
            where TU : class,
                T
            where T : class;

        /// <summary>
        /// Adds the component with lifestyle.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TU">The type of the U.</typeparam>
        /// <param name="lifestyle">The lifestyle.</param>
        /// <remarks>
        /// This method is thread-safe.
        /// </remarks>
        void AddComponentWithLifestyle<T, TU>(LifestyleType lifestyle)
            where TU : class, 
                T
            where T : class;

        /// <summary>
        /// Adds the component with lifestyle.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TU">The type of the U.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="lifestyle">The lifestyle.</param>
        /// <remarks>
        /// This method is thread-safe.
        /// </remarks>
        void AddComponentWithLifestyle<T, TU>(string key, LifestyleType lifestyle)
            where T : class
            where TU : T;

        /// <summary>
        /// Adds the component with properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="extendedProperties">The extended properties.</param>
        /// <remarks>This method is thread-safe.</remarks>
        void AddComponentWithProperties<T>(IDictionary extendedProperties)
            where T : class;

        /// <summary>
        /// Adds the component with properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="extendedProperties">The extended properties.</param>
        /// <remarks>This method is thread-safe.</remarks>
        void AddComponentWithProperties<T>(string key, IDictionary extendedProperties)
            where T : class;

        /// <summary>
        /// Adds the component with dependency (dependency can be a constructor parameter or public property).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TU">The type of the U.</typeparam>
        /// <param name="key">The key.</param>
        /// <param name="lifestyle">The lifestyle.</param>
        /// <param name="dependencyPropertyName">Name of the dependency property.</param>
        /// <param name="dependencyPropertyValue">The dependency property value.</param>
        void AddComponentWithDependency<T, TU>(string key, LifestyleType lifestyle, string dependencyPropertyName,
                                               object dependencyPropertyValue)
            where T : class
            where TU : T;

        /// <summary>
        /// Resolves the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        object Resolve(string key, IDictionary arguments);

        /// <summary>
        /// Resolves the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        object Resolve(string key, Type service);

        /// <summary>
        /// Resolves the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        object Resolve(string key);

        /// <summary>
        /// Resolves the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <returns></returns>
        object Resolve(Type service);

        /// <summary>
        /// Resolves the specified service.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        object Resolve(Type service, IDictionary arguments);

        /// <summary>
        /// Releases the specified instance.
        /// </summary>
        /// <param name="instanceParam">The instance.</param>
        void Release(object instanceParam);

        /// <summary>
        /// Resolves this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>();

        /// <summary>
        /// Resolves the specified arguments.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        T Resolve<T>(IDictionary arguments);

        /// <summary>
        /// Resolves the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T Resolve<T>(string key);

        /// <summary>
        /// Resolves the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        T Resolve<T>(string key, IDictionary arguments);

        /// <summary>
        /// Resolves the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="service">The service.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        object Resolve(string key, Type service, IDictionary arguments);

        /// <summary>
        /// Replaces the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="lifestyle">The lifestyle.</param>
        void Replace(string key, Type serviceType, Type classType, LifestyleType lifestyle = LifestyleType.Singleton);

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Adds the facility.
        /// </summary>
        /// <typeparam name="TFacility">The type of the facility.</typeparam>
        void AddFacility<TFacility>()
            where TFacility : IFacility, new();

        /// <summary>
        /// Adds the facility.
        /// </summary>
        /// <param name="facility">The facility.</param>
        void AddFacility(IFacility facility);

        /// <summary>
        /// Adds the facility.
        /// </summary>
        /// <typeparam name="TFacility">The type of the facility.</typeparam>
        /// <param name="onCreate">The on create.</param>
        void AddFacility<TFacility>(Action<TFacility> onCreate)
            where TFacility : IFacility, new();

        /// <summary>
        /// Determines whether the specified service key contains service.
        /// </summary>
        /// <param name="serviceKey">The service key.</param>
        /// <returns>
        ///   <c>true</c> if the specified service key contains service; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsService(Type serviceKey);

        /// <summary>
        /// Determines whether the specified key contains key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if the specified key contains key; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsKey(string key);
    }
}
