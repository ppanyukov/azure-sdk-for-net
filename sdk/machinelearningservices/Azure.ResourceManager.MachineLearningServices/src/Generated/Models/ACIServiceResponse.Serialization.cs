// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.MachineLearningServices.Models
{
    public partial class ACIServiceResponse : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (Optional.IsDefined(ContainerResourceRequirements))
            {
                writer.WritePropertyName("containerResourceRequirements");
                writer.WriteObjectValue(ContainerResourceRequirements);
            }
            if (Optional.IsDefined(Location))
            {
                writer.WritePropertyName("location");
                writer.WriteStringValue(Location);
            }
            if (Optional.IsDefined(AuthEnabled))
            {
                writer.WritePropertyName("authEnabled");
                writer.WriteBooleanValue(AuthEnabled.Value);
            }
            if (Optional.IsDefined(SslEnabled))
            {
                writer.WritePropertyName("sslEnabled");
                writer.WriteBooleanValue(SslEnabled.Value);
            }
            if (Optional.IsDefined(AppInsightsEnabled))
            {
                writer.WritePropertyName("appInsightsEnabled");
                writer.WriteBooleanValue(AppInsightsEnabled.Value);
            }
            if (Optional.IsDefined(DataCollection))
            {
                writer.WritePropertyName("dataCollection");
                writer.WriteObjectValue(DataCollection);
            }
            if (Optional.IsDefined(SslCertificate))
            {
                writer.WritePropertyName("sslCertificate");
                writer.WriteStringValue(SslCertificate);
            }
            if (Optional.IsDefined(SslKey))
            {
                writer.WritePropertyName("sslKey");
                writer.WriteStringValue(SslKey);
            }
            if (Optional.IsDefined(Cname))
            {
                writer.WritePropertyName("cname");
                writer.WriteStringValue(Cname);
            }
            if (Optional.IsDefined(PublicIp))
            {
                writer.WritePropertyName("publicIp");
                writer.WriteStringValue(PublicIp);
            }
            if (Optional.IsDefined(PublicFqdn))
            {
                writer.WritePropertyName("publicFqdn");
                writer.WriteStringValue(PublicFqdn);
            }
            if (Optional.IsCollectionDefined(Models))
            {
                writer.WritePropertyName("models");
                writer.WriteStartArray();
                foreach (var item in Models)
                {
                    writer.WriteObjectValue(item);
                }
                writer.WriteEndArray();
            }
            if (Optional.IsDefined(EnvironmentImageRequest))
            {
                writer.WritePropertyName("environmentImageRequest");
                writer.WriteObjectValue(EnvironmentImageRequest);
            }
            if (Optional.IsDefined(VnetConfiguration))
            {
                writer.WritePropertyName("vnetConfiguration");
                writer.WriteObjectValue(VnetConfiguration);
            }
            if (Optional.IsDefined(EncryptionProperties))
            {
                writer.WritePropertyName("encryptionProperties");
                writer.WriteObjectValue(EncryptionProperties);
            }
            if (Optional.IsDefined(Description))
            {
                writer.WritePropertyName("description");
                writer.WriteStringValue(Description);
            }
            if (Optional.IsCollectionDefined(KvTags))
            {
                writer.WritePropertyName("kvTags");
                writer.WriteStartObject();
                foreach (var item in KvTags)
                {
                    writer.WritePropertyName(item.Key);
                    writer.WriteStringValue(item.Value);
                }
                writer.WriteEndObject();
            }
            if (Optional.IsCollectionDefined(Properties))
            {
                writer.WritePropertyName("properties");
                writer.WriteStartObject();
                foreach (var item in Properties)
                {
                    writer.WritePropertyName(item.Key);
                    writer.WriteStringValue(item.Value);
                }
                writer.WriteEndObject();
            }
            writer.WritePropertyName("computeType");
            writer.WriteStringValue(ComputeType.ToString());
            if (Optional.IsDefined(DeploymentType))
            {
                writer.WritePropertyName("deploymentType");
                writer.WriteStringValue(DeploymentType.Value.ToString());
            }
            writer.WriteEndObject();
        }

        internal static ACIServiceResponse DeserializeACIServiceResponse(JsonElement element)
        {
            Optional<ContainerResourceRequirements> containerResourceRequirements = default;
            Optional<string> scoringUri = default;
            Optional<string> location = default;
            Optional<bool> authEnabled = default;
            Optional<bool> sslEnabled = default;
            Optional<bool> appInsightsEnabled = default;
            Optional<ACIServiceResponseDataCollection> dataCollection = default;
            Optional<string> sslCertificate = default;
            Optional<string> sslKey = default;
            Optional<string> cname = default;
            Optional<string> publicIp = default;
            Optional<string> publicFqdn = default;
            Optional<string> swaggerUri = default;
            Optional<IReadOnlyDictionary<string, object>> modelConfigMap = default;
            Optional<IList<Model>> models = default;
            Optional<ACIServiceResponseEnvironmentImageRequest> environmentImageRequest = default;
            Optional<ACIServiceResponseVnetConfiguration> vnetConfiguration = default;
            Optional<ACIServiceResponseEncryptionProperties> encryptionProperties = default;
            Optional<string> description = default;
            Optional<IDictionary<string, string>> kvTags = default;
            Optional<IDictionary<string, string>> properties = default;
            Optional<WebServiceState> state = default;
            Optional<ServiceResponseBaseError> error = default;
            ComputeEnvironmentType computeType = default;
            Optional<DeploymentType> deploymentType = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("containerResourceRequirements"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    containerResourceRequirements = ContainerResourceRequirements.DeserializeContainerResourceRequirements(property.Value);
                    continue;
                }
                if (property.NameEquals("scoringUri"))
                {
                    scoringUri = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("location"))
                {
                    location = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("authEnabled"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    authEnabled = property.Value.GetBoolean();
                    continue;
                }
                if (property.NameEquals("sslEnabled"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    sslEnabled = property.Value.GetBoolean();
                    continue;
                }
                if (property.NameEquals("appInsightsEnabled"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    appInsightsEnabled = property.Value.GetBoolean();
                    continue;
                }
                if (property.NameEquals("dataCollection"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    dataCollection = ACIServiceResponseDataCollection.DeserializeACIServiceResponseDataCollection(property.Value);
                    continue;
                }
                if (property.NameEquals("sslCertificate"))
                {
                    sslCertificate = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("sslKey"))
                {
                    sslKey = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("cname"))
                {
                    cname = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("publicIp"))
                {
                    publicIp = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("publicFqdn"))
                {
                    publicFqdn = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("swaggerUri"))
                {
                    swaggerUri = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("modelConfigMap"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    foreach (var property0 in property.Value.EnumerateObject())
                    {
                        dictionary.Add(property0.Name, property0.Value.GetObject());
                    }
                    modelConfigMap = dictionary;
                    continue;
                }
                if (property.NameEquals("models"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    List<Model> array = new List<Model>();
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        array.Add(Model.DeserializeModel(item));
                    }
                    models = array;
                    continue;
                }
                if (property.NameEquals("environmentImageRequest"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    environmentImageRequest = ACIServiceResponseEnvironmentImageRequest.DeserializeACIServiceResponseEnvironmentImageRequest(property.Value);
                    continue;
                }
                if (property.NameEquals("vnetConfiguration"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    vnetConfiguration = ACIServiceResponseVnetConfiguration.DeserializeACIServiceResponseVnetConfiguration(property.Value);
                    continue;
                }
                if (property.NameEquals("encryptionProperties"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    encryptionProperties = ACIServiceResponseEncryptionProperties.DeserializeACIServiceResponseEncryptionProperties(property.Value);
                    continue;
                }
                if (property.NameEquals("description"))
                {
                    description = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("kvTags"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    foreach (var property0 in property.Value.EnumerateObject())
                    {
                        dictionary.Add(property0.Name, property0.Value.GetString());
                    }
                    kvTags = dictionary;
                    continue;
                }
                if (property.NameEquals("properties"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    foreach (var property0 in property.Value.EnumerateObject())
                    {
                        dictionary.Add(property0.Name, property0.Value.GetString());
                    }
                    properties = dictionary;
                    continue;
                }
                if (property.NameEquals("state"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    state = new WebServiceState(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("error"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    error = ServiceResponseBaseError.DeserializeServiceResponseBaseError(property.Value);
                    continue;
                }
                if (property.NameEquals("computeType"))
                {
                    computeType = new ComputeEnvironmentType(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("deploymentType"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    deploymentType = new DeploymentType(property.Value.GetString());
                    continue;
                }
            }
            return new ACIServiceResponse(description.Value, Optional.ToDictionary(kvTags), Optional.ToDictionary(properties), Optional.ToNullable(state), error.Value, computeType, Optional.ToNullable(deploymentType), containerResourceRequirements.Value, scoringUri.Value, location.Value, Optional.ToNullable(authEnabled), Optional.ToNullable(sslEnabled), Optional.ToNullable(appInsightsEnabled), dataCollection.Value, sslCertificate.Value, sslKey.Value, cname.Value, publicIp.Value, publicFqdn.Value, swaggerUri.Value, Optional.ToDictionary(modelConfigMap), Optional.ToList(models), environmentImageRequest.Value, vnetConfiguration.Value, encryptionProperties.Value);
        }
    }
}
