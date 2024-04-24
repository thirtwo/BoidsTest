// All rights reserved 2024 ©
// Copyright (c) Ekrem Bugra Berdan thirtwo@protonmail.com
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Thirtwo.DI
{
    public class DependencyContainer : MonoBehaviour
    {
        private Dictionary<Type, object> dependencies = new Dictionary<Type, object>();


        void Awake()
        {
            AutoRegister();
            FetchInject();
        }
        private void FetchInject()//Monobehaviour only
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                                     .SelectMany(s => s.GetTypes())
                                     .Where(p => typeof(IResolve).IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);
            foreach (var type in types)
            {
                var instance = FindObjectOfType(type);
                ((IResolve)instance).Inject(this);
            }
        }
        private void AutoRegister() //Monobehaviour only
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                                     .SelectMany(s => s.GetTypes())
                                     .Where(p => typeof(IRegister).IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);

            foreach (var type in types)
            {
                var instance = FindObjectOfType(type);
                ((IRegister)instance).Register(this);
            }
        }

        public void Register<T>(T dependency)
        {
            Type type = typeof(T);
            if (dependencies.ContainsKey(type))
            {
                Debug.LogWarning($"Dependency of type {type.Name} is already registered.");
                return;
            }
            dependencies.Add(type, dependency);
        }

        public T Resolve<T>()
        {
            Type type = typeof(T);
            if (dependencies.ContainsKey(type))
            {
                return (T)dependencies[type];
            }
            else
            {
                Debug.LogError($"Dependency of type {type.Name} is not registered.");
                return default;
            }
        }

    }
    public interface IRegister
    {
        void Register(DependencyContainer container);
    }

    public interface IResolve
    {
        void Inject(DependencyContainer container);
    }

}
