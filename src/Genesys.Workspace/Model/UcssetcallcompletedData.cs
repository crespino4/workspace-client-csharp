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
    /// UcssetcallcompletedData
    /// </summary>
    [DataContract]
    public partial class UcssetcallcompletedData :  IEquatable<UcssetcallcompletedData>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UcssetcallcompletedData" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected UcssetcallcompletedData() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="UcssetcallcompletedData" /> class.
        /// </summary>
        /// <param name="InteractionId">The id of the interaction (required).</param>
        /// <param name="CallDuration">The duration of the call (required).</param>
        /// <param name="UserData">A key/value pairs list of the user data of the call. (required).</param>
        public UcssetcallcompletedData(string InteractionId = default(string), int? CallDuration = default(int?), List<Kvpair> UserData = default(List<Kvpair>))
        {
            // to ensure "InteractionId" is required (not null)
            if (InteractionId == null)
            {
                throw new InvalidDataException("InteractionId is a required property for UcssetcallcompletedData and cannot be null");
            }
            else
            {
                this.InteractionId = InteractionId;
            }
            // to ensure "CallDuration" is required (not null)
            if (CallDuration == null)
            {
                throw new InvalidDataException("CallDuration is a required property for UcssetcallcompletedData and cannot be null");
            }
            else
            {
                this.CallDuration = CallDuration;
            }
            // to ensure "UserData" is required (not null)
            if (UserData == null)
            {
                throw new InvalidDataException("UserData is a required property for UcssetcallcompletedData and cannot be null");
            }
            else
            {
                this.UserData = UserData;
            }
        }
        
        /// <summary>
        /// The id of the interaction
        /// </summary>
        /// <value>The id of the interaction</value>
        [DataMember(Name="interactionId", EmitDefaultValue=false)]
        public string InteractionId { get; set; }

        /// <summary>
        /// The duration of the call
        /// </summary>
        /// <value>The duration of the call</value>
        [DataMember(Name="callDuration", EmitDefaultValue=false)]
        public int? CallDuration { get; set; }

        /// <summary>
        /// A key/value pairs list of the user data of the call.
        /// </summary>
        /// <value>A key/value pairs list of the user data of the call.</value>
        [DataMember(Name="userData", EmitDefaultValue=false)]
        public List<Kvpair> UserData { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UcssetcallcompletedData {\n");
            sb.Append("  InteractionId: ").Append(InteractionId).Append("\n");
            sb.Append("  CallDuration: ").Append(CallDuration).Append("\n");
            sb.Append("  UserData: ").Append(UserData).Append("\n");
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
            return this.Equals(obj as UcssetcallcompletedData);
        }

        /// <summary>
        /// Returns true if UcssetcallcompletedData instances are equal
        /// </summary>
        /// <param name="other">Instance of UcssetcallcompletedData to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(UcssetcallcompletedData other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.InteractionId == other.InteractionId ||
                    this.InteractionId != null &&
                    this.InteractionId.Equals(other.InteractionId)
                ) && 
                (
                    this.CallDuration == other.CallDuration ||
                    this.CallDuration != null &&
                    this.CallDuration.Equals(other.CallDuration)
                ) && 
                (
                    this.UserData == other.UserData ||
                    this.UserData != null &&
                    this.UserData.SequenceEqual(other.UserData)
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
                if (this.InteractionId != null)
                    hash = hash * 59 + this.InteractionId.GetHashCode();
                if (this.CallDuration != null)
                    hash = hash * 59 + this.CallDuration.GetHashCode();
                if (this.UserData != null)
                    hash = hash * 59 + this.UserData.GetHashCode();
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