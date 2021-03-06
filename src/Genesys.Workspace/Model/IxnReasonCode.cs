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
    /// IxnReasonCode
    /// </summary>
    [DataContract]
    public partial class IxnReasonCode :  IEquatable<IxnReasonCode>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IxnReasonCode" /> class.
        /// </summary>
        /// <param name="ReasonSystemName">ReasonSystemName.</param>
        /// <param name="ReasonDescription">ReasonDescription.</param>
        /// <param name="Reason">Reason.</param>
        public IxnReasonCode(string ReasonSystemName = default(string), string ReasonDescription = default(string), int? Reason = default(int?))
        {
            this.ReasonSystemName = ReasonSystemName;
            this.ReasonDescription = ReasonDescription;
            this.Reason = Reason;
        }
        
        /// <summary>
        /// Gets or Sets ReasonSystemName
        /// </summary>
        [DataMember(Name="reasonSystemName", EmitDefaultValue=false)]
        public string ReasonSystemName { get; set; }

        /// <summary>
        /// Gets or Sets ReasonDescription
        /// </summary>
        [DataMember(Name="reasonDescription", EmitDefaultValue=false)]
        public string ReasonDescription { get; set; }

        /// <summary>
        /// Gets or Sets Reason
        /// </summary>
        [DataMember(Name="reason", EmitDefaultValue=false)]
        public int? Reason { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class IxnReasonCode {\n");
            sb.Append("  ReasonSystemName: ").Append(ReasonSystemName).Append("\n");
            sb.Append("  ReasonDescription: ").Append(ReasonDescription).Append("\n");
            sb.Append("  Reason: ").Append(Reason).Append("\n");
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
            return this.Equals(obj as IxnReasonCode);
        }

        /// <summary>
        /// Returns true if IxnReasonCode instances are equal
        /// </summary>
        /// <param name="other">Instance of IxnReasonCode to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(IxnReasonCode other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.ReasonSystemName == other.ReasonSystemName ||
                    this.ReasonSystemName != null &&
                    this.ReasonSystemName.Equals(other.ReasonSystemName)
                ) && 
                (
                    this.ReasonDescription == other.ReasonDescription ||
                    this.ReasonDescription != null &&
                    this.ReasonDescription.Equals(other.ReasonDescription)
                ) && 
                (
                    this.Reason == other.Reason ||
                    this.Reason != null &&
                    this.Reason.Equals(other.Reason)
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
                if (this.ReasonSystemName != null)
                    hash = hash * 59 + this.ReasonSystemName.GetHashCode();
                if (this.ReasonDescription != null)
                    hash = hash * 59 + this.ReasonDescription.GetHashCode();
                if (this.Reason != null)
                    hash = hash * 59 + this.Reason.GetHashCode();
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
