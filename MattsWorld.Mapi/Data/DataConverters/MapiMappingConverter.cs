using System;
using System.Collections.Generic;
using MattsWorld.Mapi.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MattsWorld.Mapi.Data.DataConverters
{
    class MapiMappingDataConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            JObject jObject = JObject.Load(reader);

            Guid id = new Guid(jObject["Id"].Value<string>());

            if (!(jObject["Maps"] is JArray mapArray))
                return new MapiMapping(id);

            List<MapiMap> maps = new List<MapiMap>();
            foreach (JToken jToken in mapArray)
            {
                string from = jToken["From"].Value<string>();
                string type = jToken["Type"].Value<string>();
                string to = jToken["To"].Value<string>();
                string meta = jToken["Meta"].Value<string>();
                maps.Add(new MapiMap(from,type,to, meta));
            }

            return new MapiMapping(id, maps);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(MapiMapping) == objectType;
        }
    }
}
