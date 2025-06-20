﻿using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace CommonHelpers.Common;

/// <summary>
/// A dependency-free JSON serializer and deserializer.
/// NOTE: When using this, you need to decorate the model with [DataContract] and the properties with [DataMember] attributes
/// </summary>
/// <typeparam name="T"></typeparam>
public static class JsonHelper<T> where T : class
{
    /// <summary>
    /// Serializes an object to JSON
    /// </summary>
    public static string Serialize(T instance)
    {
        if (instance == null)
            throw new ArgumentNullException(nameof(instance));
        var serializer = new DataContractJsonSerializer(typeof(T));

        using var stream = new MemoryStream();

        serializer.WriteObject(stream, instance);
        return Encoding.Default.GetString(stream.ToArray());
    }

    /// <summary>
    /// DeSerializes an object from JSON
    /// </summary>
    public static T Deserialize(string json)
    {
        if (string.IsNullOrEmpty(json))
            throw new ArgumentNullException(nameof(json));

        try
        {
            using var stream = new MemoryStream(Encoding.Default.GetBytes(json));
            var serializer = new DataContractJsonSerializer(typeof(T));
            return serializer.ReadObject(stream) as T;
        }
        catch (Exception ex)
        {
            throw new SerializationException("Input string is not valid JSON or does not match the expected type.", ex);
        }
    }
}