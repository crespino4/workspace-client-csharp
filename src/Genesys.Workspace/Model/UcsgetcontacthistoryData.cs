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
    /// UcsgetcontacthistoryData
    /// </summary>
    [DataContract]
    public partial class UcsgetcontacthistoryData :  IEquatable<UcsgetcontacthistoryData>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UcsgetcontacthistoryData" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected UcsgetcontacthistoryData() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="UcsgetcontacthistoryData" /> class.
        /// </summary>
        /// <param name="ContactId">The id of the contact (required).</param>
        /// <param name="CustomAttributes">The list of custom contact attributes to be returned for each interaction of the contact.</param>
        /// <param name="FromDate">The date from which the interactions should be retrieved (UCS format).</param>
        /// <param name="ToDate">The date to which the interactions should be retrieved (UCS format).</param>
        public UcsgetcontacthistoryData(string ContactId = default(string), List<string> CustomAttributes = default(List<string>), string FromDate = default(string), string ToDate = default(string))
        {
            // to ensure "ContactId" is required (not null)
            if (ContactId == null)
            {
                throw new InvalidDataException("ContactId is a required property for UcsgetcontacthistoryData and cannot be null");
            }
            else
            {
                this.ContactId = ContactId;
            }
            this.CustomAttributes = CustomAttributes;
            this.FromDate = FromDate;
            this.ToDate = ToDate;
        }
        
        /// <summary>
        /// The id of the contact
        /// </summary>
        /// <value>The id of the contact</value>
        [DataMember(Name="contactId", EmitDefaultValue=false)]
        public string ContactId { get; set; }

        /// <summary>
        /// The list of custom contact attributes to be returned for each interaction of the contact
        /// </summary>
        /// <value>The list of custom contact attributes to be returned for each interaction of the contact</value>
        [DataMember(Name="customAttributes", EmitDefaultValue=false)]
        public List<string> CustomAttributes { get; set; }

        /// <summary>
        /// The date from which the interactions should be retrieved (UCS format)
        /// </summary>
        /// <value>The date from which the interactions should be retrieved (UCS format)</value>
        [DataMember(Name="fromDate", EmitDefaultValue=false)]
        public string FromDate { get; set; }

        /// <summary>
        /// The date to which the interactions should be retrieved (UCS format)
        /// </summary>
        /// <value>The date to which the interactions should be retrieved (UCS format)</value>
        [DataMember(Name="toDate", EmitDefaultValue=false)]
        public string ToDate { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UcsgetcontacthistoryData {\n");
            sb.Append("  ContactId: ").Append(ContactId).Append("\n");
            sb.Append("  CustomAttributes: ").Append(CustomAttributes).Append("\n");
            sb.Append("  FromDate: ").Append(FromDate).Append("\n");
            sb.Append("  ToDate: ").Append(ToDate).Append("\n");
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
            return this.Equals(obj as UcsgetcontacthistoryData);
        }

        /// <summary>
        /// Returns true if UcsgetcontacthistoryData instances are equal
        /// </summary>
        /// <param name="other">Instance of UcsgetcontacthistoryData to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(UcsgetcontacthistoryData other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.ContactId == other.ContactId ||
                    this.ContactId != null &&
                    this.ContactId.Equals(other.ContactId)
                ) && 
                (
                    this.CustomAttributes == other.CustomAttributes ||
                    this.CustomAttributes != null &&
                    this.CustomAttributes.SequenceEqual(other.CustomAttributes)
                ) && 
                (
                    this.FromDate == other.FromDate ||
                    this.FromDate != null &&
                    this.FromDate.Equals(other.FromDate)
                ) && 
                (
                    this.ToDate == other.ToDate ||
                    this.ToDate != null &&
                    this.ToDate.Equals(other.ToDate)
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
                if (this.CustomAttributes != null)
                    hash = hash * 59 + this.CustomAttributes.GetHashCode();
                if (this.FromDate != null)
                    hash = hash * 59 + this.FromDate.GetHashCode();
                if (this.ToDate != null)
                    hash = hash * 59 + this.ToDate.GetHashCode();
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
