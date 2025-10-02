using PAW3.Architecture.Helpers;

namespace PAW3.Architecture;

/// <summary>
/// Interface defining methods for RESTful operations.
/// </summary>
public interface IRestProvider
{
	/// <summary>
	/// Deletes a resource asynchronously.
	/// </summary>
	/// <param name="endpoint">The endpoint for the DELETE request.</param>
	/// <param name="id">The ID of the resource to delete.</param>
	/// <returns>A task that represents the asynchronous operation, containing the response as a string.</returns>
	Task<string> DeleteAsync(string endpoint, string id);

	/// <summary>
	/// Retrieves a resource asynchronously.
	/// </summary>
	/// <param name="endpoint">The endpoint for the GET request.</param>
	/// <param name="id">The ID of the resource to retrieve. Can be null if not applicable.</param>
	/// <returns>A task that represents the asynchronous operation, containing the response as a string.</returns>
	Task<string> GetAsync(string endpoint, string? id);

	/// <summary>
	/// Creates a resource asynchronously.
	/// </summary>
	/// <param name="endpoint">The endpoint for the POST request.</param>
	/// <param name="content">The content to send in the request body.</param>
	/// <returns>A task that represents the asynchronous operation, containing the response as a string.</returns>
	Task<string> PostAsync(string endpoint, string content);

	/// <summary>
	/// Updates a resource asynchronously.
	/// </summary>
	/// <param name="endpoint">The endpoint for the PUT request.</param>
	/// <param name="requestUri">The URI of the resource to update.</param>
	/// <param name="content">The content to send in the request body.</param>
	/// <returns>A task that represents the asynchronous operation, containing the response as a string.</returns>
	Task<string> PutAsync(string endpoint, string id, string content);
}

/// <summary>
/// Implementation of the IRestProvider interface, providing methods for RESTful operations.
/// </summary>
public class RestProvider : IRestProvider
{
	/// <summary>
	/// Retrieves a resource asynchronously.
	/// </summary>
	/// <param name="endpoint">The endpoint for the GET request.</param>
	/// <param name="id">The ID of the resource to retrieve. Can be null if not applicable.</param>
	/// <returns>A task that represents the asynchronous operation, containing the response as a string.</returns>
	public async Task<string> GetAsync(string endpoint, string? id)
	{
		try
		{
			var response = await RestProviderHelpers.CreateHttpClient(endpoint)
				.GetAsync(id);
			return await RestProviderHelpers.GetResponse(response);
		}
		catch (Exception ex)
		{
			throw RestProviderHelpers.ThrowError(endpoint, ex);
		}
	}

	/// <summary>
	/// Creates a resource asynchronously.
	/// </summary>
	/// <param name="endpoint">The endpoint for the POST request.</param>
	/// <param name="content">The content to send in the request body.</param>
	/// <returns>A task that represents the asynchronous operation, containing the response as a string.</returns>
	public async Task<string> PostAsync(string endpoint, string content)
	{
		try
		{
			var response = await RestProviderHelpers.CreateHttpClient(endpoint)
				.PostAsync(endpoint, RestProviderHelpers.CreateContent(content));
			var result = await RestProviderHelpers.GetResponse(response);
			return result;
		}
		catch (Exception ex)
		{
			throw RestProviderHelpers.ThrowError(endpoint, ex);
		}
	}

	/// <summary>
	/// Updates a resource asynchronously.
	/// </summary>
	/// <param name="endpoint">The endpoint for the PUT request.</param>
	/// <param name="id">The ID of the resource to update.</param>
	/// <param name="content">The content to send in the request body.</param>
	/// <returns>A task that represents the asynchronous operation, containing the response as a string.</returns>
	public async Task<string> PutAsync(string endpoint, string id, string content)
	{
		try
		{
			var response = await RestProviderHelpers.CreateHttpClient(endpoint)
				.PutAsync(id, RestProviderHelpers.CreateContent(content));
			var result = await RestProviderHelpers.GetResponse(response);
			return result;
		}
		catch (Exception ex)
		{
			throw RestProviderHelpers.ThrowError(endpoint, ex);
		}
	}

	/// <summary>
	/// Deletes a resource asynchronously.
	/// </summary>
	/// <param name="endpoint">The endpoint for the DELETE request.</param>
	/// <param name="id">The ID of the resource to delete.</param>
	/// <returns>A task that represents the asynchronous operation, containing the response as a string.</returns>
	public async Task<string> DeleteAsync(string endpoint, string id)
	{
		try
		{
			var response = await RestProviderHelpers.CreateHttpClient(endpoint)
				.DeleteAsync(id);
			var result = await RestProviderHelpers.GetResponse(response);
			return result;
		}
		catch (Exception ex)
		{
			throw RestProviderHelpers.ThrowError(endpoint, ex);
		}
	}
}
