// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

#pragma warning disable 1591

namespace IdentityModel;

[EditorBrowsable(EditorBrowsableState.Never)]
public class X509CertificatesFinder(StoreLocation location, StoreName name, X509FindType findType)
{
    private readonly StoreLocation _location = location;
    private readonly StoreName _name = name;
    private readonly X509FindType _findType = findType;

    public IEnumerable<X509Certificate2> Find(object findValue, bool validOnly = true)
    {
        using var store = new X509Store(_name, _location);
        store.Open(OpenFlags.ReadOnly);

        var certColl = store.Certificates.Find(_findType, findValue, validOnly);
        return certColl.Cast<X509Certificate2>();
    }
}