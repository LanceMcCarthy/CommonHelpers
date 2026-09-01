using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CommonHelpers.Extensions;

public static class EnumExtensions
{
    public static List<T> GetEnumAsList<T>()
    {
        var array = Enum.GetValues(typeof(T));

        return [.. array.Cast<T>()];
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