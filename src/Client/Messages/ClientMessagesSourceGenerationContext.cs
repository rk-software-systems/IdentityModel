﻿using System.Text.Json.Serialization;

namespace IdentityModel.Client;

[JsonSourceGenerationOptions(
    WriteIndented = false,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Metadata,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(DynamicClientRegistrationDocument))]
internal sealed partial class ClientMessagesSourceGenerationContext : JsonSerializerContext
{
}
