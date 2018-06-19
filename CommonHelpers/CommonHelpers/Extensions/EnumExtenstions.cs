using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CommonHelpers.Extensions
{
    public static class EnumExtenstions
    {
        public static List<T> GetEnumAsList<T>()
        {
            var array = Enum.GetValues(typeof(T));

            var list = new List<T>();

            foreach (var item in array)
            {
                list.Add((T) item);
            }

            return list;
        }
        
        public static T GetEnumDefaultValue<T>()
        {
            var defaultValue = typeof(T)
                .GetRuntimeFields()
                .FirstOrDefault(x => x.GetCustomAttribute(typeof(DefaultValueAttribute)) != null);

            if (defaultValue == null) 
                return default(T);

            return (T) Enum.Parse(typeof(T), defaultValue.Name);
        }
    }
}
