using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace VirtualBingo.UI.Shared.Converters
{
    public class PathToFileNameJsonConverter : JsonConverter
    {
        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if(CanConvert(value?.GetType()))
            {
                new JValue(Path.GetFileName(value.ToString())).WriteTo(writer);
            }
            else
            {
                JToken.FromObject(value).WriteTo(writer);
            }
        }
    }
}
