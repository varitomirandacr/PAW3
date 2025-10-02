using System.Net.Http.Headers;
using System.Text;

namespace PAW3.Architecture.Helpers;

/// <summary>
/// Provides helper methods for creating and managing HTTP requests.
/// </summary>
internal static class RestProviderHelpers
{
	/// <summary>
	/// Creates an <see cref="HttpClient"/> with the specified base address.
	/// </summary>
	/// <param name="endpoint">The base URL for the HTTP client.</param>
	/// <returns>A configured instance of <see cref="HttpClient"/>.</returns>
	internal static HttpClient CreateHttpClient(string endpoint)
	{
		var client = new HttpClient { BaseAddress = new Uri(endpoint) };
		client.DefaultRequestHeaders.Accept.Clear();
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		return client;
	}

	/// <summary>
	/// Creates a <see cref="StringContent"/> object for the specified content.
	/// </summary>
	/// <param name="content">The content to be sent in the HTTP request body.</param>
	/// <returns>A <see cref="StringContent"/> object with the specified content.</returns>
	internal static StringContent CreateContent(string content) => new(content, Encoding.UTF8, "application/json");

	/// <summary>
	/// Reads the response from an HTTP request and ensures the response was successful.
	/// </summary>
	/// <param name="response">The <see cref="HttpResponseMessage"/> to read the content from.</param>
	/// <returns>A task that represents the asynchronous operation, containing the response content as a string.</returns>
	/// <exception cref="HttpRequestException">Thrown if the response indicates a failure.</exception>
	internal static async Task<string> GetResponse(HttpResponseMessage response)
	{
		response.EnsureSuccessStatusCode();
		return await response.Content.ReadAsStringAsync();
	}

	/// <summary>
	/// Creates an <see cref="ApplicationException"/> with details about an error occurring during data retrieval.
	/// </summary>
	/// <param name="endpoint">The endpoint where the error occurred.</param>
	/// <param name="ex">The original exception.</param>
	/// <returns>An <see cref="ApplicationException"/> describing the error.</returns>
	internal static Exception ThrowError(string endpoint, Exception ex) => new ApplicationException($"Error getting data from {endpoint}", ex);
}
