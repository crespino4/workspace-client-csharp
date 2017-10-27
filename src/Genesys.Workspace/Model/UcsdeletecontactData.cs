/* 
 * Workspace API
 *
 * Agent API
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = Genesys.Workspace.Client.SwaggerDateConverter;

namespace Genesys.Workspace.Model
{
    /// <summary>
    /// UcsdeletecontactData
    /// </summary>
    [DataContract]
    public partial class UcsdeletecontactData :  IEquatable<UcsdeletecontactData>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UcsdeletecontactData" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected UcsdeletecontactData() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="UcsdeletecontactData" /> class.
        /// </summary>
        /// <param name="ContactId">The id of the contact to be deleted (required).</param>
        public UcsdeletecontactData(string ContactId = default(string))
        {
            // to ensure "ContactId" is required (not null)
            if (ContactId == null)
            {
                throw new InvalidDataException("ContactId is a required property for UcsdeletecontactData and cannot be null");
            }
            else
            {
                this.ContactId = ContactId;
            }
        }
        
        /// <summary>
        /// The id of the contact to be deleted
        /// </summary>
        /// <value>The id of the contact to be deleted</value>
        [DataMember(Name="contactId", EmitDefaultValue=false)]
        public string ContactId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UcsdeletecontactData {\n");
            sb.Append("  ContactId: ").Append(ContactId).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as UcsdeletecontactData);
        }

        /// <summary>
        /// Returns true if UcsdeletecontactData instances are equal
        /// </summary>
        /// <param name="other">Instance of UcsdeletecontactData to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(UcsdeletecontactData other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.ContactId == other.ContactId ||
                    this.ContactId != null &&
                    this.ContactId.Equals(other.ContactId)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)
                if (this.ContactId != null)
                    hash = hash * 59 + this.ContactId.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}