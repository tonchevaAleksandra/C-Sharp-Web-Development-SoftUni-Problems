using SUS.HTTP;
using System;

namespace SUS.MvcFramework
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public abstract class BaseHttpAttribute : Attribute
    {
        public string Url { get; set; }

        public abstract HttpMethod Method { get; }
    }
}