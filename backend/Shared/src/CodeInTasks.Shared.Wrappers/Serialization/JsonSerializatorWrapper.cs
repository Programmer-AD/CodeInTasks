using System.Text;
using System.Text.Json;
using CodeInTasks.Shared.Wrappers.Interfaces;

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

        public string Serialize<T>(T value)
        {
            var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(value, serializerOptions);
            var jsonString = Encoding.UTF8.GetString(jsonBytes);

            return jsonString;
        }
    }
}
