using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

// ReSharper disable InconsistentNaming

namespace Elasmobranch.Dictionaries
{
    public class HashTable<K, V>
    {
        private readonly KeyValuePair[] _data;

        public HashTable(int length = 100)
        {
            if (typeof(K).GetMethod("ToString")?.DeclaringType != typeof(object))
                throw new Exception(
                    "Cannot create a HashTable for an object that does not implement `ToString()` method.");
            
            _data = new KeyValuePair[length];
        }

        public void Put(K key, V value)
        {
            var index = ComputeHash(key) % _data.Length;
            if (_data[index] != null)
            {
                do
                {
                    index = (index + 1) % _data.Length;
                } while (_data[index] == null);
            }
            _data[index] = new KeyValuePair(key, value);
        }

        public V Get(K key)
        {
            var index = ComputeHash(key) % _data.Length;
            // search until the there is an empty slot OR we find the key OR until we have searched the whole list
            while (!_data[index % _data.Length].Equals(null) && !_data[index % _data.Length].Key.Equals(key) && index != index % _data.Length + _data.Length) index++;
            return _data[index % _data.Length].Value;
        }

        public V Get(K key, V defaultValue)
        {
            try
            {
                return Get(key);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     Compute the integer hash of anything
        /// </summary>
        /// <param name="key">anything</param>
        /// <see href="https://stackoverflow.com/a/425184" />
        /// <returns>An integer hash</returns>
        public static int ComputeHash(object key)
        {
            return ObjectToByteArray(key).Aggregate(0, (current, b) => (current * 31) ^ b);
        }


        /// <summary>
        ///     Convert anything into a byte array
        /// </summary>
        /// <seealso href="https://stackoverflow.com/a/4865123" />
        /// <param name="obj">Literally anything</param>
        /// <returns>A byte array</returns>
        private static IEnumerable<byte> ObjectToByteArray([NotNull] object obj)
        {
            var bf = new BinaryFormatter();
            using var ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        private class KeyValuePair
        {
            public readonly K Key;
            public readonly V Value;
            
            public KeyValuePair(K key, V value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}