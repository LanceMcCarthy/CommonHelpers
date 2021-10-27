using System;

namespace CommonHelpers.Common
{
    /// <summary>
    /// Example:  Singleton<MyMenuItem>.Instance.IconPath
    /// </summary>
    /// <typeparam name="T">The Type of the singleton</typeparam>
    public static class Singleton<T> where T : new()
    {
        private static readonly Lazy<T> instance = new Lazy<T>(() => new T());

        public static T Instance => instance.Value;
    }
}
