using System.Text;
using System.Text.Json;

namespace CodeInTasks.Shared.Wrappers.Serialization
{
    internal class JsonSerializerWrapper : IJsonSerializer
    {
        private static readonly JsonSerializerOptions serializerOptions = new(JsonSerializerDefaults.Web);

        public T Deserialize<T>(string json)
        {
            var result = JsonSerializer.Deserialize<T>(json, serializerOptions);

            return result;
        }

        public bool TryDeserialize<T>(string value, out T result)
        {
            try
            {
                result = Deserialize<T>(value);

                return true;
            }
            catch (JsonException)
            {
                result = default;

                return false;
            }
        }

        public string Serialize<T>(T value)
        {
            var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(value, serializerOptions);
            var jsonString = Encoding.UTF8.GetString(jsonBytes);

            return jsonString;
        }


    }
}
