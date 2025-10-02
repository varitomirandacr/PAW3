using System.Net.Http;

namespace PAW3.Architecture.Extensions;

/// <summary>
/// Provides extension methods for <see cref="HttpClient"/>.
/// </summary>
public static class HttpClientExtensions
{
	/// <summary>
	/// Adds a default request header to the <see cref="HttpClient"/>. If the header already exists, it is replaced.
	/// </summary>
	/// <param name="client">The <see cref="HttpClient"/> instance to which the header will be added.</param>
	/// <param name="name">The name of the header to add.</param>
	/// <param name="value">The value of the header to add.</param>
	private static void AddDefaultRequestHeader(this HttpClient client, string name, string value)
	{
		var defaultHeaders = client.DefaultRequestHeaders;
		if (defaultHeaders.Contains(name))
			defaultHeaders.Remove(name);
		defaultHeaders.Add(name, value);
	}
}
