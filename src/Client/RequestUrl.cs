// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel.Internal;

namespace IdentityModel.Client;

/// <summary>
/// Helper class for creating request URLs
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="RequestUrl"/> class.
/// </remarks>
/// <param name="baseUrl">The authorize endpoint.</param>
public class RequestUrl(string baseUrl)
{
    private readonly string _baseUrl = baseUrl;

    /// <summary>
    /// Creates URL based on key/value input pairs.
    /// </summary>
    /// <param name="parameters">The query string parameters.</param>
    /// <returns></returns>
    public string Create(Parameters parameters)
    {
        if (parameters == null || !parameters.Any())
        {
            return _baseUrl;
        }

        return QueryHelpers.AddQueryString(_baseUrl, parameters);
    }
}