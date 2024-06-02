// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Text;
using System.Text.Encodings.Web;

namespace IdentityModel.Internal;

internal static class QueryHelpers
{
    /// <summary>
    /// Append the given query key and value to the URI.
    /// </summary>
    /// <param name="uri">The base URI.</param>
    /// <param name="name">The name of the query key.</param>
    /// <param name="value">The query value.</param>
    /// <returns>The combined result.</returns>
    public static string AddQueryString(string uri, string name, string value)
    {
        ArgumentNullException.ThrowIfNull(uri, nameof(uri));

        ArgumentNullException.ThrowIfNull(name, nameof(name));

        ArgumentNullException.ThrowIfNull(value, nameof(value));

        return AddQueryString(uri,
            [
                new KeyValuePair<string, string>(name, value)
            ]);
    }

    /// <summary>
    /// Append the given query keys and values to the uri.
    /// </summary>
    /// <param name="uri">The base uri.</param>
    /// <param name="queryString">A collection of name value query pairs to append.</param>
    /// <returns>The combined result.</returns>
    public static string AddQueryString(string uri, IDictionary<string, string> queryString)
    {
        ArgumentNullException.ThrowIfNull(uri, nameof(uri));

        ArgumentNullException.ThrowIfNull(queryString, nameof(queryString));

        return AddQueryString(uri, (IEnumerable<KeyValuePair<string, string>>)queryString);
    }

    public static string AddQueryString(
        string uri,
        IEnumerable<KeyValuePair<string, string>> queryString)
    {
        ArgumentNullException.ThrowIfNull(uri, nameof(uri));

        ArgumentNullException.ThrowIfNull(queryString, nameof(queryString));

        var anchorIndex = uri.IndexOf('#', StringComparison.Ordinal);
        var uriToBeAppended = uri;
        var anchorText = "";
        // If there is an anchor, then the query string must be inserted before its first occurance.
        if (anchorIndex != -1)
        {
            anchorText = uri.Substring(anchorIndex);
            uriToBeAppended = uri.Substring(0, anchorIndex);
        }

        var hasQuery = uriToBeAppended.Contains('?', StringComparison.Ordinal);

        var sb = new StringBuilder();
        sb.Append(uriToBeAppended);
        foreach (var parameter in queryString)
        {
            if (parameter.Value == null) continue;

            sb.Append(hasQuery ? '&' : '?');
            sb.Append(UrlEncoder.Default.Encode(parameter.Key));
            sb.Append('=');
            sb.Append(UrlEncoder.Default.Encode(parameter.Value));
            hasQuery = true;
        }

        sb.Append(anchorText);
        return sb.ToString();
    }
}