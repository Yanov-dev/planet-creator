using System;
using System.Drawing;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PlanetCreator.Engine.Converters
{
    public class JsonColorConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var color = (Color) value;

            writer.WriteRawValue($"\"#{color.R:X2}{color.G:X2}{color.B:X2}\"");
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var raw = JToken.Load(reader);

            var colorStr = raw.ToString();

            return Color.FromArgb(
                int.Parse(colorStr.Substring(1, 2), NumberStyles.HexNumber),
                int.Parse(colorStr.Substring(3, 2), NumberStyles.HexNumber),
                int.Parse(colorStr.Substring(5, 2), NumberStyles.HexNumber));
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Color) == objectType;
        }
    }
}