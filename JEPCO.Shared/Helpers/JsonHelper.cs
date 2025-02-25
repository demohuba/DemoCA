using System.Text.Encodings.Web;
using System.Text.Json;

namespace JEPCO.Shared.Helpers
{
    public static class JsonHelper
    {
        public static T Deserialize<T>(string obj, bool returnNullInsteadOfException = false)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(obj, GetSerilizationOptions());
            }
            catch
            {
                if (returnNullInsteadOfException)
                    return default(T);
                throw;
            }
        }

        public static object Deserialize(string obj, Type type)
        {
            return JsonSerializer.Deserialize(obj, type, GetSerilizationOptions());
        }

        public static string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj, GetSerilizationOptions());
        }

        public static T DynamicToObject<T>(dynamic obj)
        {
            string jsonString = JsonSerializer.Serialize(obj, GetSerilizationOptions());
            return JsonHelper.Deserialize<T>(jsonString);
        }

        public static JsonSerializerOptions GetSerilizationOptions()
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                Encoder = JavaScriptEncoder.Default,
                PropertyNamingPolicy = null,
            };

            options.Converters.Add(new TimeSpanToStringConverter());
            options.Converters.Add(new NullableConverterFactory());

            return options;
        }
    }
}
