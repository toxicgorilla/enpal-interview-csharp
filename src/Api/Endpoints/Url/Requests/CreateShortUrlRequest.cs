﻿namespace Api.Endpoints.Url.Requests;

/// <summary>
/// Request model for the <see cref="CreateShortUrlRequest"/>.
/// </summary>
public class CreateShortUrlRequest
{
    /// <summary>
    /// The long url to shorten.
    /// </summary>
    public string Url { get; set; } = default!;
}
