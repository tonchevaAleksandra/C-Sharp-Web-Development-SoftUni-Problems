using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SIS.MvcFramework
{
    public class ServiceCollection : IServiceCollection
    {
        private IDictionary<Type, Type> dependancyContainer = new ConcurrentDictionary<Type, Type>();
        public void Add<TSource, TDestination>()
        where TDestination : TSource
        {
            dependancyContainer[typeof(TSource)] = typeof(TDestination);
        }

        public object CreateInstance(Type type)
        {
            if (dependancyContainer.ContainsKey(type))
            {
                type = dependancyContainer[type];
            }
            var constructor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                .OrderBy(x => x.GetParameters().Count())
                .FirstOrDefault();
            var parameterValues = new List<object>();
            foreach (var parameter in constructor.GetParameters())
            {
                var instance = CreateInstance(parameter.ParameterType);
                parameterValues.Add(instance);
            }
            return constructor.Invoke(parameterValues.ToArray());
        }

        public T CreateInstance<T>()
        {
            return (T)this.CreateInstance(typeof(T));
        }
    }
}
