using System.Text.Json;

namespace IdentityModel.Client;

/// <summary>
/// MTLS endpoint aliases
/// </summary>
/// <remarks>
/// ctor
/// </remarks>
/// <param name="json"></param>
public class MtlsEndpointAliases(JsonElement json)
{
    /// <summary>
    /// The raw JSON
    /// </summary>
    public JsonElement Json { get; } = json;

    /// <summary>
    /// Returns the token endpoint address
    /// </summary>
    public string? TokenEndpoint => Json.TryGetString(OidcConstants.Discovery.TokenEndpoint);
        
    /// <summary>
    /// Returns the revocation endpoint address
    /// </summary>
    public string? RevocationEndpoint => Json.TryGetString(OidcConstants.Discovery.RevocationEndpoint);
        
    /// <summary>
    /// Returns the device authorization endpoint address
    /// </summary>
    public string? DeviceAuthorizationEndpoint => Json.TryGetString(OidcConstants.Discovery.DeviceAuthorizationEndpoint);
        
    /// <summary>
    /// Returns the introspection endpoint address
    /// </summary>
    public string? IntrospectionEndpoint => Json.TryGetString(OidcConstants.Discovery.IntrospectionEndpoint);
        
}