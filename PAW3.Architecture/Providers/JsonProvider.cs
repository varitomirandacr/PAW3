using System.Text;
using System.Text.Json;

namespace PAW3.Architecture.Providers;

/// <summary>
/// Provides methods for serializing and deserializing JSON data.
/// </summary>
public class JsonProvider
{
	/// <summary>
	/// Asynchronously deserializes a byte array to an object of type T.
	/// </summary>
	/// <typeparam name="T">The type of the object to deserialize to.</typeparam>
	/// <param name="bytes">The byte array containing the JSON data.</param>
	/// <returns>A task that represents the asynchronous operation, containing the deserialized object of type T.</returns>
	public static async Task<T> DeserializeAsync<T>(byte[] bytes) where T : class
	{
		using MemoryStream stream = new(bytes);
		T? deserialized = await JsonSerializer.DeserializeAsync<T>(stream);
		return deserialized!;
	}

	/// <summary>
	/// Deserializes a JSON string to an object of type T.
	/// </summary>
	/// <typeparam name="T">The type of the object to deserialize to.</typeparam>
	/// <param name="content">The JSON string to deserialize.</param>
	/// <returns>The deserialized object of type T, or null if the content was not valid.</returns>
	public static T? DeserializeSimple<T>(string content) where T : class
	{
		return JsonSerializer.Deserialize<T>(content, GetJsonSerializerOptions());
	}

	/// <summary>
	/// Asynchronously deserializes a JSON string to an object of type T.
	/// </summary>
	/// <typeparam name="T">The type of the object to deserialize to.</typeparam>
	/// <param name="content">The JSON string to deserialize.</param>
	/// <returns>A task that represents the asynchronous operation, containing the deserialized object of type T.</returns>
	public static async Task<T> DeserializeAsync<T>(string content) where T : class
	{
		byte[] bytes = Encoding.UTF8.GetBytes(content);
		return await DeserializeAsync<T>(bytes);
	}

	/// <summary>
	/// Serializes an object to a JSON string.
	/// </summary>
	/// <param name="content">The object to serialize.</param>
	/// <returns>A JSON string representation of the object.</returns>
	public static string Serialize(object content)
	{
		var serialized = JsonSerializer.Serialize(content);
		return serialized;
	}

	/// <summary>
	/// Gets the JSON serializer options used for deserialization.
	/// </summary>
	/// <returns>A JsonSerializerOptions instance with specific settings.</returns>
	private static JsonSerializerOptions GetJsonSerializerOptions()
	{
		return new JsonSerializerOptions
		{
			AllowTrailingCommas = true,
			PropertyNameCaseInsensitive = true,
		};
	}
}
