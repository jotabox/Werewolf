using System;
using System.Collections.Generic;
using UnityEngine;

namespace Werewolf.Core
{
    public class ServiceLocator
    {
        private static ServiceLocator _current;
        public static ServiceLocator Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new ServiceLocator();
                }
                return _current;
            }
        }

        private readonly Dictionary<Type, IGameService> _services =
            new Dictionary<Type, IGameService>();

        public void Register<T>(T service) where T : IGameService
        {
            var type = typeof(T);

            if (_services.ContainsKey(type))
            {
                Debug.LogWarning($"Service {type.Name} already registered.");
                return;
            }

            _services[type] = service;
        }

        public T Get<T>() where T : IGameService
        {
            var type = typeof(T);

            if (_services.TryGetValue(type, out var service))
            {
                return (T)service;
            }

            Debug.LogError($"Service {type.Name} not found.");
            return default;
        }

        public void Clear()
        {
            _services.Clear();
        }
    }
}
