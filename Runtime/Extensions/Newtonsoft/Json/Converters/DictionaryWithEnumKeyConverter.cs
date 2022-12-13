using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UniSharper.Extensions
{
    /// <summary>
    /// Converts an <see cref="Dictionary{TKey,TValue}"/> object with <see cref="Enum"/> key to and from JSON.
    /// </summary>
    /// <typeparam name="TKey">The type definition of key in <see cref="Dictionary{TKey,TValue}"/>. </typeparam>
    /// <typeparam name="TValue">The type definition of value in <see cref="Dictionary{TKey,TValue}"/></typeparam>
    public class DictionaryWithEnumKeyConverter<TKey, TValue> : JsonConverter where TKey : Enum
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is not Dictionary<TKey, TValue> dictionary)
                return;

            writer.WriteStartObject();

            foreach (var pair in dictionary)
            {
                writer.WritePropertyName(Convert.ToInt32(pair.Key).ToString());
                serializer.Serialize(writer, pair.Value);
            }

            writer.WriteEndObject();
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var result = new Dictionary<TKey, TValue>();
            var jObject = JObject.Load(reader);

            foreach (var pair in jObject)
            {
                int.TryParse(pair.Key, out var keyIntValue);
                var key = (TKey) Enum.ToObject(typeof(TKey), keyIntValue);
                if (pair.Value == null) 
                    continue;
                
                var value = (TValue) pair.Value.ToObject(typeof(TValue));
                result.Add(key, value);
            }

            return result;
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType) => typeof(IDictionary<TKey, TValue>) == objectType;
    }
}