// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel.Internal;
using System.Text;
using System.Text.Json;

namespace IdentityModel.Client;

/// <summary>
/// HttpClient extensions for dynamic registration
/// </summary>
public static class HttpClientDynamicRegistrationExtensions
{
    /// <summary>
    /// Send a dynamic registration request.
    /// </summary>
    /// <param name="client">The client.</param>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public static async Task<DynamicClientRegistrationResponse> RegisterClientAsync(
        this HttpMessageInvoker client, DynamicClientRegistrationRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(client, nameof(client));

        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var clone = request.Clone();

        clone.Method = HttpMethod.Post;
        clone.Content = new StringContent(
            JsonSerializer.Serialize(request.Document, ClientMessagesSourceGenerationContext.Default.DynamicClientRegistrationDocument),
            Encoding.UTF8,
            "application/json");
        clone.Prepare();

        if (!string.IsNullOrWhiteSpace(request.Token))
        {
            clone.SetBearerToken(request.Token);
        }

        HttpResponseMessage response;
        try
        {
            response = await client.SendAsync(clone, cancellationToken).ConfigureAwait();
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            return ProtocolResponse.FromException<DynamicClientRegistrationResponse>(ex);
        }

        return await ProtocolResponse.FromHttpResponseAsync<DynamicClientRegistrationResponse>(response).ConfigureAwait();
    }
}