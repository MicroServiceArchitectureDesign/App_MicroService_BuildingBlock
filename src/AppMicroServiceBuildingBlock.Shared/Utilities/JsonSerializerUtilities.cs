using System.Text.Json;

namespace AppMicroServiceBuildingBlock.Shared.Utilities;
public static class JsonSerializerUtilities
{
    public static JsonSerializerOptions SerializerOptions => new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static string Serialize<T>(T item)
    {
        return JsonSerializer.Serialize(item, options: SerializerOptions);
    }

}
