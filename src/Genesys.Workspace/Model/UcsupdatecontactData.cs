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
    /// UcsupdatecontactData
    /// </summary>
    [DataContract]
    public partial class UcsupdatecontactData :  IEquatable<UcsupdatecontactData>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UcsupdatecontactData" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected UcsupdatecontactData() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="UcsupdatecontactData" /> class.
        /// </summary>
        /// <param name="ContactId">The id of the contact (required).</param>
        /// <param name="AddedProperties">The list of contact attributes to be added to the contact (required).</param>
        /// <param name="ChangedProperties">The list of contact attributes to be updated for the contact (required).</param>
        /// <param name="DeletedProperties">The list of contact attributes to be deleted for the contact (required).</param>
        public UcsupdatecontactData(string ContactId = default(string), List<Object> AddedProperties = default(List<Object>), List<Object> ChangedProperties = default(List<Object>), List<Object> DeletedProperties = default(List<Object>))
        {
            // to ensure "ContactId" is required (not null)
            if (ContactId == null)
            {
                throw new InvalidDataException("ContactId is a required property for UcsupdatecontactData and cannot be null");
            }
            else
            {
                this.ContactId = ContactId;
            }
            // to ensure "AddedProperties" is required (not null)
            if (AddedProperties == null)
            {
                throw new InvalidDataException("AddedProperties is a required property for UcsupdatecontactData and cannot be null");
            }
            else
            {
                this.AddedProperties = AddedProperties;
            }
            // to ensure "ChangedProperties" is required (not null)
            if (ChangedProperties == null)
            {
                throw new InvalidDataException("ChangedProperties is a required property for UcsupdatecontactData and cannot be null");
            }
            else
            {
                this.ChangedProperties = ChangedProperties;
            }
            // to ensure "DeletedProperties" is required (not null)
            if (DeletedProperties == null)
            {
                throw new InvalidDataException("DeletedProperties is a required property for UcsupdatecontactData and cannot be null");
            }
            else
            {
                this.DeletedProperties = DeletedProperties;
            }
        }
        
        /// <summary>
        /// The id of the contact
        /// </summary>
        /// <value>The id of the contact</value>
        [DataMember(Name="contactId", EmitDefaultValue=false)]
        public string ContactId { get; set; }

        /// <summary>
        /// The list of contact attributes to be added to the contact
        /// </summary>
        /// <value>The list of contact attributes to be added to the contact</value>
        [DataMember(Name="addedProperties", EmitDefaultValue=false)]
        public List<Object> AddedProperties { get; set; }

        /// <summary>
        /// The list of contact attributes to be updated for the contact
        /// </summary>
        /// <value>The list of contact attributes to be updated for the contact</value>
        [DataMember(Name="changedProperties", EmitDefaultValue=false)]
        public List<Object> ChangedProperties { get; set; }

        /// <summary>
        /// The list of contact attributes to be deleted for the contact
        /// </summary>
        /// <value>The list of contact attributes to be deleted for the contact</value>
        [DataMember(Name="deletedProperties", EmitDefaultValue=false)]
        public List<Object> DeletedProperties { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UcsupdatecontactData {\n");
            sb.Append("  ContactId: ").Append(ContactId).Append("\n");
            sb.Append("  AddedProperties: ").Append(AddedProperties).Append("\n");
            sb.Append("  ChangedProperties: ").Append(ChangedProperties).Append("\n");
            sb.Append("  DeletedProperties: ").Append(DeletedProperties).Append("\n");
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
            return this.Equals(obj as UcsupdatecontactData);
        }

        /// <summary>
        /// Returns true if UcsupdatecontactData instances are equal
        /// </summary>
        /// <param name="other">Instance of UcsupdatecontactData to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(UcsupdatecontactData other)
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
                    this.AddedProperties == other.AddedProperties ||
                    this.AddedProperties != null &&
                    this.AddedProperties.SequenceEqual(other.AddedProperties)
                ) && 
                (
                    this.ChangedProperties == other.ChangedProperties ||
                    this.ChangedProperties != null &&
                    this.ChangedProperties.SequenceEqual(other.ChangedProperties)
                ) && 
                (
                    this.DeletedProperties == other.DeletedProperties ||
                    this.DeletedProperties != null &&
                    this.DeletedProperties.SequenceEqual(other.DeletedProperties)
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
                if (this.AddedProperties != null)
                    hash = hash * 59 + this.AddedProperties.GetHashCode();
                if (this.ChangedProperties != null)
                    hash = hash * 59 + this.ChangedProperties.GetHashCode();
                if (this.DeletedProperties != null)
                    hash = hash * 59 + this.DeletedProperties.GetHashCode();
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
