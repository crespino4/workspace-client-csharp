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
    /// VoicecallsidmergeData
    /// </summary>
    [DataContract]
    public partial class VoicecallsidmergeData :  IEquatable<VoicecallsidmergeData>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VoicecallsidmergeData" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected VoicecallsidmergeData() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="VoicecallsidmergeData" /> class.
        /// </summary>
        /// <param name="OtherConnId">Connection id of the other call to merge with (required).</param>
        /// <param name="Reasons">A key/value pairs list of a data structure that provides additional information associated with this action..</param>
        /// <param name="Extensions">A key/value pairs list of additional data..</param>
        public VoicecallsidmergeData(string OtherConnId = default(string), List<Kvpair> Reasons = default(List<Kvpair>), List<Kvpair> Extensions = default(List<Kvpair>))
        {
            // to ensure "OtherConnId" is required (not null)
            if (OtherConnId == null)
            {
                throw new InvalidDataException("OtherConnId is a required property for VoicecallsidmergeData and cannot be null");
            }
            else
            {
                this.OtherConnId = OtherConnId;
            }
            this.Reasons = Reasons;
            this.Extensions = Extensions;
        }
        
        /// <summary>
        /// Connection id of the other call to merge with
        /// </summary>
        /// <value>Connection id of the other call to merge with</value>
        [DataMember(Name="otherConnId", EmitDefaultValue=false)]
        public string OtherConnId { get; set; }

        /// <summary>
        /// A key/value pairs list of a data structure that provides additional information associated with this action.
        /// </summary>
        /// <value>A key/value pairs list of a data structure that provides additional information associated with this action.</value>
        [DataMember(Name="reasons", EmitDefaultValue=false)]
        public List<Kvpair> Reasons { get; set; }

        /// <summary>
        /// A key/value pairs list of additional data.
        /// </summary>
        /// <value>A key/value pairs list of additional data.</value>
        [DataMember(Name="extensions", EmitDefaultValue=false)]
        public List<Kvpair> Extensions { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class VoicecallsidmergeData {\n");
            sb.Append("  OtherConnId: ").Append(OtherConnId).Append("\n");
            sb.Append("  Reasons: ").Append(Reasons).Append("\n");
            sb.Append("  Extensions: ").Append(Extensions).Append("\n");
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
            return this.Equals(obj as VoicecallsidmergeData);
        }

        /// <summary>
        /// Returns true if VoicecallsidmergeData instances are equal
        /// </summary>
        /// <param name="other">Instance of VoicecallsidmergeData to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(VoicecallsidmergeData other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.OtherConnId == other.OtherConnId ||
                    this.OtherConnId != null &&
                    this.OtherConnId.Equals(other.OtherConnId)
                ) && 
                (
                    this.Reasons == other.Reasons ||
                    this.Reasons != null &&
                    this.Reasons.SequenceEqual(other.Reasons)
                ) && 
                (
                    this.Extensions == other.Extensions ||
                    this.Extensions != null &&
                    this.Extensions.SequenceEqual(other.Extensions)
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
                if (this.OtherConnId != null)
                    hash = hash * 59 + this.OtherConnId.GetHashCode();
                if (this.Reasons != null)
                    hash = hash * 59 + this.Reasons.GetHashCode();
                if (this.Extensions != null)
                    hash = hash * 59 + this.Extensions.GetHashCode();
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
