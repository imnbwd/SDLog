using System.IO;
using System.Text;

namespace SDLog
{
    public class JsonConverter
    {
        public static Encoding Encoding = Encoding.UTF8;

        public static T DeserializeFromContent<T>(string content)
        {
            T obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content, new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            });

            return obj;
        }

        public static T DeserializeFromFile<T>(string filePath)
        {
            T obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath, Encoding), new Newtonsoft.Json.JsonSerializerSettings
            {
                StringEscapeHandling = Newtonsoft.Json.StringEscapeHandling.Default,
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            });

            return obj;
        }

        public static string SerializeToContent(object obj)
        {
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            Stream objectStream;
            using (objectStream = new MemoryStream())
            {
                using (StreamWriter writter = new StreamWriter(objectStream, Encoding))
                {
                    using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(writter))
                    {
                        serializer.Serialize(writer, obj);

                        objectStream.Seek(0, SeekOrigin.Begin);
                    }
                }
            }

            using (StreamReader sr = new StreamReader(objectStream))
            {
                return sr.ReadToEnd();
            }
        }

        public static void SerializeToFile(object obj, string filePath)
        {
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding))
                {
                    using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
                    {
                        writer.Formatting = Newtonsoft.Json.Formatting.Indented;
                        serializer.Serialize(writer, obj);
                    }
                }
            }
        }
    }
}