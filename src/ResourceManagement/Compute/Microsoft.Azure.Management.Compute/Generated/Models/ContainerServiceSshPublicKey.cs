// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
// 
// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.Management.Compute.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Microsoft.Rest.Azure;

    /// <summary>
    /// Contains information about SSH certificate public key data.
    /// </summary>
    public partial class ContainerServiceSshPublicKey
    {
        /// <summary>
        /// Initializes a new instance of the ContainerServiceSshPublicKey
        /// class.
        /// </summary>
        public ContainerServiceSshPublicKey() { }

        /// <summary>
        /// Initializes a new instance of the ContainerServiceSshPublicKey
        /// class.
        /// </summary>
        public ContainerServiceSshPublicKey(string keyData)
        {
            KeyData = keyData;
        }

        /// <summary>
        /// Gets or sets Certificate public key used to authenticate with VM
        /// through SSH. The certificate must be in Pem format with or
        /// without headers.
        /// </summary>
        [JsonProperty(PropertyName = "keyData")]
        public string KeyData { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (KeyData == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "KeyData");
            }
        }
    }
}
