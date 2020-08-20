// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.Sql.Models
{
    public partial class EncryptionProtectorListResult
    {
        internal static EncryptionProtectorListResult DeserializeEncryptionProtectorListResult(JsonElement element)
        {
            Optional<IReadOnlyList<EncryptionProtector>> value = default;
            Optional<string> nextLink = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("value"))
                {
                    List<EncryptionProtector> array = new List<EncryptionProtector>();
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        array.Add(EncryptionProtector.DeserializeEncryptionProtector(item));
                    }
                    value = array;
                    continue;
                }
                if (property.NameEquals("nextLink"))
                {
                    nextLink = property.Value.GetString();
                    continue;
                }
            }
            return new EncryptionProtectorListResult(Optional.ToList(value), nextLink.Value);
        }
    }
}