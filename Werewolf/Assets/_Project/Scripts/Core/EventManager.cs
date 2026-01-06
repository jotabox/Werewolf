using System;
using System.Collections.Generic;
using UnityEngine;

namespace Werewolf.Core
{
    public static class EventManager
    {
        private static readonly Dictionary<Type, Action<IGameEvent>> _listeners =
            new Dictionary<Type, Action<IGameEvent>>();

        public static void AddListener<T>(Action<T> listener) where T : IGameEvent
        {
            var type = typeof(T);

            if (_listeners.TryGetValue(type, out var existing))
            {
                _listeners[type] = existing + (e => listener((T)e));
            }
            else
            {
                _listeners[type] = e => listener((T)e);
            }
        }

        public static void RemoveListener<T>(Action<T> listener) where T : IGameEvent
        {
            var type = typeof(T);

            if (_listeners.TryGetValue(type, out var existing))
            {
                existing -= e => listener((T)e);
                _listeners[type] = existing;
            }
        }

        public static void Raise(IGameEvent gameEvent)
        {
            var type = gameEvent.GetType();

            if (_listeners.TryGetValue(type, out var listeners))
            {
                listeners?.Invoke(gameEvent);
            }
        }

        public static void Clear()
        {
            _listeners.Clear();
        }
    }
}
