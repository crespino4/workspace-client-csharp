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
    /// UcslucenesearchData
    /// </summary>
    [DataContract]
    public partial class UcslucenesearchData :  IEquatable<UcslucenesearchData>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UcslucenesearchData" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected UcslucenesearchData() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="UcslucenesearchData" /> class.
        /// </summary>
        /// <param name="MaxResults">The maximum number of contacts to be returned.</param>
        /// <param name="Query">The query to do the lucene search for contacts (required).</param>
        /// <param name="CustomAttributes">The list of custom contact attributes to be returned for each contact in response.</param>
        public UcslucenesearchData(int? MaxResults = default(int?), string Query = default(string), List<string> CustomAttributes = default(List<string>))
        {
            // to ensure "Query" is required (not null)
            if (Query == null)
            {
                throw new InvalidDataException("Query is a required property for UcslucenesearchData and cannot be null");
            }
            else
            {
                this.Query = Query;
            }
            this.MaxResults = MaxResults;
            this.CustomAttributes = CustomAttributes;
        }
        
        /// <summary>
        /// The maximum number of contacts to be returned
        /// </summary>
        /// <value>The maximum number of contacts to be returned</value>
        [DataMember(Name="maxResults", EmitDefaultValue=false)]
        public int? MaxResults { get; set; }

        /// <summary>
        /// The query to do the lucene search for contacts
        /// </summary>
        /// <value>The query to do the lucene search for contacts</value>
        [DataMember(Name="query", EmitDefaultValue=false)]
        public string Query { get; set; }

        /// <summary>
        /// The list of custom contact attributes to be returned for each contact in response
        /// </summary>
        /// <value>The list of custom contact attributes to be returned for each contact in response</value>
        [DataMember(Name="customAttributes", EmitDefaultValue=false)]
        public List<string> CustomAttributes { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UcslucenesearchData {\n");
            sb.Append("  MaxResults: ").Append(MaxResults).Append("\n");
            sb.Append("  Query: ").Append(Query).Append("\n");
            sb.Append("  CustomAttributes: ").Append(CustomAttributes).Append("\n");
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
            return this.Equals(obj as UcslucenesearchData);
        }

        /// <summary>
        /// Returns true if UcslucenesearchData instances are equal
        /// </summary>
        /// <param name="other">Instance of UcslucenesearchData to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(UcslucenesearchData other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.MaxResults == other.MaxResults ||
                    this.MaxResults != null &&
                    this.MaxResults.Equals(other.MaxResults)
                ) && 
                (
                    this.Query == other.Query ||
                    this.Query != null &&
                    this.Query.Equals(other.Query)
                ) && 
                (
                    this.CustomAttributes == other.CustomAttributes ||
                    this.CustomAttributes != null &&
                    this.CustomAttributes.SequenceEqual(other.CustomAttributes)
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
                if (this.MaxResults != null)
                    hash = hash * 59 + this.MaxResults.GetHashCode();
                if (this.Query != null)
                    hash = hash * 59 + this.Query.GetHashCode();
                if (this.CustomAttributes != null)
                    hash = hash * 59 + this.CustomAttributes.GetHashCode();
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
