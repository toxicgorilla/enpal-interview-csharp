namespace Api.Endpoints.Url.Requests;

/// <summary>
/// Request model for the <see cref="RedirectToUrlRequest"/> endpoint.
/// </summary>
public class RedirectToUrlRequest
{
    /// <summary>
    /// The unique identifier of the shortened URL.
    /// </summary>
    public string Id { get; set; } = default!;
}
