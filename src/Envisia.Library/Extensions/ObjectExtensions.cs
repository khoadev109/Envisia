using Newtonsoft.Json;
using System.Reflection;
using System.Xml.Serialization;

namespace Envisia.Library.Extensions
{
    public static class ObjectExtensions
    {
        public static T CloneViaJson<T>(this T o)
        {
            var json = o.ToJson();

            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string ToJson<T>(this T o)
        {
            return JsonConvert.SerializeObject(o);
        }

        public static string ToJsonFormatted<T>(this T o)
        {
            return JsonConvert.SerializeObject(o, new JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented } );
        }

        public static T FromJson<T>(this string text)
        {
            return JsonConvert.DeserializeObject<T>(text);
        }

        public static T FromJsonFile<T>(this string path)
        {
            var text = File.ReadAllText(path);

            return text.FromJson<T>();
        }

        public static string ToXml<T>(this T toSerialize)
        {
            var xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (var textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);

                return textWriter.ToString();
            }
        }

        public static T FromXml<T>(this string s) where T : class
        {            
            var serializer = new XmlSerializer(typeof(T));

            using (TextReader reader = new StringReader(s))
            {
                return (T)serializer.Deserialize(reader);
            }            
        }
    }
}
