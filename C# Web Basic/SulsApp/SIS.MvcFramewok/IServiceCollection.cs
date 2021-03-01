using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.MvcFramework
{
   public interface  IServiceCollection
   {
       void Add<TSource, TDestination>();

       object CreateInstance(Type type);

       T CreateInstance<T>();
   }
}
