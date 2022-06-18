namespace CodeInTasks.Shared.Wrappers.Interfaces
{
    public interface IJsonSerializer
    {
        string Serialize<T>(T json);

        T Deserialize<T>(string value);

        bool TryDeserialize<T>(string value, out T result);
    }
}
