﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace WinForms.Framework.Messaging
{
    public class EventAggregator : IEventAggregator
    {
        private readonly ConcurrentDictionary<Type, List<object>> _subscriptions =
            new ConcurrentDictionary<Type, List<object>>();

        private EventAggregator()
        {
        }

        public static IEventAggregator Instance { get; } = new EventAggregator();

        public void Publish<T>(T message) where T : IApplicationEvent
        {
            List<object> subscribers;
            if (_subscriptions.TryGetValue(typeof(T), out subscribers))
            {
                // To Array creates a copy in case someone unsubscribes in their own handler
                foreach (var subscriber in subscribers.ToArray())
                {
                    ((Action<T>) subscriber)(message);
                }
            }
        }

        public void Subscribe<T>(Action<T> action) where T : IApplicationEvent
        {
            var subscribers = _subscriptions.GetOrAdd(typeof(T), t => new List<object>());
            lock (subscribers)
            {
                subscribers.Add(action);
            }
        }

        public void Unsubscribe<T>(Action<T> action) where T : IApplicationEvent
        {
            List<object> subscribers;
            if (_subscriptions.TryGetValue(typeof(T), out subscribers))
            {
                lock (subscribers)
                {
                    subscribers.Remove(action);
                }
            }
        }

        public void Dispose()
        {
            _subscriptions.Clear();
        }
    }
}