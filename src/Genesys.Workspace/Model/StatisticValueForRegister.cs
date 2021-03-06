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
    /// StatisticValueForRegister
    /// </summary>
    [DataContract]
    public partial class StatisticValueForRegister :  IEquatable<StatisticValueForRegister>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticValueForRegister" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected StatisticValueForRegister() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticValueForRegister" /> class.
        /// </summary>
        /// <param name="ObjectId">ID of the object to get the statistic for (required).</param>
        /// <param name="ObjectType">Type of the obejct to get the statistic for (required).</param>
        /// <param name="Name">Name of the statistic (required).</param>
        public StatisticValueForRegister(string ObjectId = default(string), string ObjectType = default(string), string Name = default(string))
        {
            // to ensure "ObjectId" is required (not null)
            if (ObjectId == null)
            {
                throw new InvalidDataException("ObjectId is a required property for StatisticValueForRegister and cannot be null");
            }
            else
            {
                this.ObjectId = ObjectId;
            }
            // to ensure "ObjectType" is required (not null)
            if (ObjectType == null)
            {
                throw new InvalidDataException("ObjectType is a required property for StatisticValueForRegister and cannot be null");
            }
            else
            {
                this.ObjectType = ObjectType;
            }
            // to ensure "Name" is required (not null)
            if (Name == null)
            {
                throw new InvalidDataException("Name is a required property for StatisticValueForRegister and cannot be null");
            }
            else
            {
                this.Name = Name;
            }
        }
        
        /// <summary>
        /// ID of the object to get the statistic for
        /// </summary>
        /// <value>ID of the object to get the statistic for</value>
        [DataMember(Name="objectId", EmitDefaultValue=false)]
        public string ObjectId { get; set; }

        /// <summary>
        /// Type of the obejct to get the statistic for
        /// </summary>
        /// <value>Type of the obejct to get the statistic for</value>
        [DataMember(Name="objectType", EmitDefaultValue=false)]
        public string ObjectType { get; set; }

        /// <summary>
        /// Name of the statistic
        /// </summary>
        /// <value>Name of the statistic</value>
        [DataMember(Name="name", EmitDefaultValue=false)]
        public string Name { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class StatisticValueForRegister {\n");
            sb.Append("  ObjectId: ").Append(ObjectId).Append("\n");
            sb.Append("  ObjectType: ").Append(ObjectType).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
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
            return this.Equals(obj as StatisticValueForRegister);
        }

        /// <summary>
        /// Returns true if StatisticValueForRegister instances are equal
        /// </summary>
        /// <param name="other">Instance of StatisticValueForRegister to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(StatisticValueForRegister other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.ObjectId == other.ObjectId ||
                    this.ObjectId != null &&
                    this.ObjectId.Equals(other.ObjectId)
                ) && 
                (
                    this.ObjectType == other.ObjectType ||
                    this.ObjectType != null &&
                    this.ObjectType.Equals(other.ObjectType)
                ) && 
                (
                    this.Name == other.Name ||
                    this.Name != null &&
                    this.Name.Equals(other.Name)
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
                if (this.ObjectId != null)
                    hash = hash * 59 + this.ObjectId.GetHashCode();
                if (this.ObjectType != null)
                    hash = hash * 59 + this.ObjectType.GetHashCode();
                if (this.Name != null)
                    hash = hash * 59 + this.Name.GetHashCode();
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
