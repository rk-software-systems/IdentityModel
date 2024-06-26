﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Globalization;
using System.Net.Http.Headers;
using System.Text;

namespace System.Net.Http;

/// <summary>
/// HTTP Basic Authentication authorization header
/// </summary>
/// <seealso cref="AuthenticationHeaderValue" />
/// <remarks>
/// Initializes a new instance of the <see cref="BasicAuthenticationHeaderValue"/> class.
/// </remarks>
/// <param name="userName">Name of the user.</param>
/// <param name="password">The password.</param>
public class BasicAuthenticationHeaderValue(string userName, string password) : AuthenticationHeaderValue("Basic", EncodeCredential(userName, password))
{

    /// <summary>
    /// Encodes the credential.
    /// </summary>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">userName</exception>
    public static string EncodeCredential(string userName, string password)
    {
        if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException(nameof(userName));
        if (password == null) password = "";

        Encoding encoding = Encoding.UTF8;
        string credential = String.Format(CultureInfo.InvariantCulture,"{0}:{1}", userName, password);

        return Convert.ToBase64String(encoding.GetBytes(credential));
    }
}