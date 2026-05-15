using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPGInterfaces
{
    /// <summary>
    /// A simple service locator to manage global dependencies and reduce coupling.
    /// Allows systems to register and retrieve managers via their interfaces.
    /// </summary>
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        /// <summary>
        /// Registers a service instance against its type (usually an interface).
        /// </summary>
        /// <typeparam name="T">The type/interface of the service.</typeparam>
        /// <param name="service">The instance of the service.</param>
        public static void RegisterService<T>(T service)
        {
            Type type = typeof(T);
            if (!services.ContainsKey(type))
            {
                services.Add(type, service);
            }
            else
            {
                // Optionally log a warning or overwrite
                Debug.LogWarning($"[ServiceLocator] Service of type {type.Name} is already registered. Overwriting.");
                services[type] = service;
            }
        }

        /// <summary>
        /// Retrieves a registered service instance by its type.
        /// </summary>
        /// <typeparam name="T">The type/interface of the service to retrieve.</typeparam>
        /// <returns>The service instance, or default/null if not found.</returns>
        public static T GetService<T>()
        {
            Type type = typeof(T);
            if (services.TryGetValue(type, out object service))
            {
                return (T)service;
            }

            Debug.LogError($"[ServiceLocator] Service of type {type.Name} has not been registered!");
            return default(T);
        }

        /// <summary>
        /// Unregisters a service. Should be called when a manager is destroyed.
        /// </summary>
        /// <typeparam name="T">The type/interface of the service.</typeparam>
        public static void UnregisterService<T>()
        {
            Type type = typeof(T);
            if (services.ContainsKey(type))
            {
                services.Remove(type);
            }
        }
    }
}
