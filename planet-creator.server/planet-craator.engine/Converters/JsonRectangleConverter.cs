using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace planet_craator.engine.Converters
{
    public class JsonRectangleConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var rect = (Rectangle) value;
            var obj = new JObject
            {
                {"x", rect.X},
                {"y", rect.Y},
                {"width", rect.Width},
                {"height", rect.Height}
            };

            obj.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var item = JObject.Load(reader);
            return new Rectangle(
                item["x"].Value<int>(),
                item["y"].Value<int>(),
                item["width"].Value<int>(),
                item["height"].Value<int>()
            );
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Rectangle) == objectType;
        }
    }
}