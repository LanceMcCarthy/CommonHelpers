using System;
using System.Collections.Concurrent;

namespace CommonHelpers.Common
{
    /// <summary>
    /// Example:  Singleton<MyMenuItem>.Instance.IconPath
    /// </summary>
    /// <typeparam name="T">The Type of the singleton</typeparam>
    public static class Singleton<T> 
        where T : new()
    {
        private static readonly ConcurrentDictionary<Type, T> Instances = new ConcurrentDictionary<Type, T>();

        public static T Instance
        {
            get
            {
                return Instances.GetOrAdd(typeof(T), (t) => new T());
            }
        }
    }
}
